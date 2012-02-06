using System;
using System.Collections.Generic;
using System.Linq;

using Docary.Services;
using Docary.ViewModels.Desktop;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Desktop
{
    public class HomeAssembler : IHomeAssembler
    {
        private IEntryService _entryService;
        private ITimeService _timeService;
        private IUserSettingsService _userSettingsService;        

        private const int SECONDS_IN_DAY = 24 * 3600;

        public HomeAssembler(
            IEntryService entryService, 
            ITimeService timeService,
            IUserSettingsService userSettingService)
        {
            _entryService = entryService;
            _timeService = timeService;
            _userSettingsService = userSettingService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(string userId)
        {              
            var homeIndexViewModel = new HomeIndexViewModel();

            var userTimeZone = _userSettingsService.Get(userId).TimeZone;

            SetDefaultDatesWhenEmpty(homeIndexViewModel, userTimeZone);

            return AssembleHomeIndexViewModel(homeIndexViewModel, userId);
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(string userId, DateTime from, DateTime to)
        {
            var homeIndexViewModel = new HomeIndexViewModel(from, to);                                 

            return AssembleHomeIndexViewModel(homeIndexViewModel, userId);
        }        

        // TODO: Wow, this got ugly fast. Can this be simplified?
        public HomeIndexViewModel AssembleHomeIndexViewModel(HomeIndexViewModel homeIndexViewModelIn, string userId)
        {
            var indexViewModelResult = new HomeIndexViewModel(homeIndexViewModelIn.From, homeIndexViewModelIn.To);

            var userTimeZone = _userSettingsService.Get(userId).TimeZone;            

            SetDefaultDatesWhenEmpty(indexViewModelResult, userTimeZone);            
            
            // You have to take the userTimeZoneOffset into account, otherwise we end up with too little data           
            var userTimeZoneOffsetToTakeIntoAccountFrom = 
                userTimeZone.BaseUtcOffset.TotalSeconds < 0 ? new TimeSpan() : userTimeZone.BaseUtcOffset;
            var userTimeZoneOffsetToTakeIntoAccountTo = 
                userTimeZone.BaseUtcOffset.TotalSeconds > 0 ? userTimeZone.BaseUtcOffset : new TimeSpan();
            // Get the entries based on universal time
            var entries = _entryService.GetEntries(
                indexViewModelResult.From.Value.ToUniversalTime().Subtract(userTimeZoneOffsetToTakeIntoAccountFrom),
                indexViewModelResult.To.Value.ToUniversalTime().Add(userTimeZoneOffsetToTakeIntoAccountTo),
                userId);
            var start = indexViewModelResult.From.Value;
            var stop = indexViewModelResult.To.Value;

            LoadLegenda(indexViewModelResult, entries);
           
            while (start < stop)
            {
                var startOfTheDay = start.Date;
                var endOfTheDay = start.Date.AddDays(1);
                
                foreach (var entry in entries)
                {
                    var entryCreatedOnUserTimeZone = TimeZoneInfo.ConvertTimeFromUtc(entry.CreatedOn, userTimeZone);
                    var entryStoppedOnUserTimeZone = (DateTime?)DateTime.MaxValue;
                    if (entry.StoppedOn.HasValue)
                        entryStoppedOnUserTimeZone = TimeZoneInfo.ConvertTimeFromUtc(entry.StoppedOn.Value, userTimeZone);
                    
                    if (entryStoppedOnUserTimeZone < startOfTheDay || 
                        entryCreatedOnUserTimeZone > endOfTheDay)
                        continue;

                    HomeIndexViewModelEntry viewModelEntry = null;
                    
                    if (entryCreatedOnUserTimeZone <= startOfTheDay && entryStoppedOnUserTimeZone >= endOfTheDay)
                    {                        
                        viewModelEntry = new HomeIndexViewModelEntry(100, startOfTheDay, endOfTheDay);
                    }                    
                    else if (entryCreatedOnUserTimeZone <= startOfTheDay)
                    {
                        var diff = entryStoppedOnUserTimeZone.Value.Subtract(startOfTheDay);
                        var diffPercent = (diff.TotalSeconds / SECONDS_IN_DAY) * 100;

                        viewModelEntry = new HomeIndexViewModelEntry(
                            diffPercent, startOfTheDay, entryStoppedOnUserTimeZone.Value);                        
                    }                    
                    else if (entryStoppedOnUserTimeZone.Value >= endOfTheDay)
                    {
                        var diff = endOfTheDay.Subtract(entryCreatedOnUserTimeZone);
                        var diffPercent = (diff.TotalSeconds / SECONDS_IN_DAY) * 100;

                        viewModelEntry = new HomeIndexViewModelEntry(
                            diffPercent, entryCreatedOnUserTimeZone, endOfTheDay);                                   
                    }                    
                    else
                    {                     
                        var diff = entryStoppedOnUserTimeZone.Value.Subtract(entryCreatedOnUserTimeZone);
                        var diffPercent = (diff.TotalSeconds / SECONDS_IN_DAY) * 100;

                        viewModelEntry = new HomeIndexViewModelEntry(
                            diffPercent, entryCreatedOnUserTimeZone, entryStoppedOnUserTimeZone.Value);                        
                    }

                    viewModelEntry.Tag = entry.Tag == null ? string.Empty : entry.Tag.TitleCasedName;
                    viewModelEntry.Color = entry.Tag == null ? string.Empty : entry.Tag.Color;
                    viewModelEntry.Title = string.Format("{0} ({1}-{2}): {3}", 
                        new object[] { viewModelEntry.Tag, viewModelEntry.Start.ToShortTimeString(), viewModelEntry.End.ToShortTimeString(), entry.Description });

                    if (indexViewModelResult.EntryGroups.Any(eg => eg.Date == startOfTheDay))
                    {
                        var oldEntryGroup = indexViewModelResult.EntryGroups.Where(eg => eg.Date == startOfTheDay).First();
                        var newEntryGroup = new HomeIndexViewModelEntryGroup(oldEntryGroup.Date);                        
                        
                        newEntryGroup.Entries = oldEntryGroup.Entries;
                        newEntryGroup.Entries.Add(viewModelEntry);

                        indexViewModelResult.EntryGroups.RemoveAll(eg => eg.Date == startOfTheDay);
                        indexViewModelResult.EntryGroups.Add(newEntryGroup);
                    }
                    else
                    {                  
                        var entryGroup = new HomeIndexViewModelEntryGroup(startOfTheDay);                                                
                        
                        entryGroup.Entries.Add(viewModelEntry);
                        indexViewModelResult.EntryGroups.Add(entryGroup);
                    }
                }

                start = start.AddDays(1);
            }

            return indexViewModelResult;
        }

        private void SetDefaultDatesWhenEmpty(HomeIndexViewModel indexViewModelResult, TimeZoneInfo userTimeZone)
        {
            var nowDate = _timeService.GetNow();
            var defaultFromDate = nowDate.AddDays(-14);
            var defaultToDate = nowDate.AddDays(1);

            if (!indexViewModelResult.From.HasValue)
                indexViewModelResult.From = TimeZoneInfo.ConvertTimeFromUtc(defaultFromDate, userTimeZone).Date;
            if (!indexViewModelResult.To.HasValue)
                indexViewModelResult.To = TimeZoneInfo.ConvertTimeFromUtc(defaultToDate, userTimeZone).Date;
        }

        private void LoadLegenda(HomeIndexViewModel indexViewModel, IEnumerable<Entry> entries)
        {
            var tags =  entries.Select(e => e.Tag).Distinct();

            indexViewModel.Legenda = 
                tags.Select(t => new HomeIndexViewModelLegendaTag(t.TitleCasedName, t.Color))
                    .OrderBy(t => t.Name)
                    .ToList();
        }
    }
}
