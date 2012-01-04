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
            var userTimeZone = _userSettingsService.Get(userId).TimeZone;
            var defaultFrom = TimeZoneInfo.ConvertTimeFromUtc(GetDefaultFromDate(), userTimeZone).Date;
            var defaultTo = TimeZoneInfo.ConvertTimeFromUtc(GetDefaultToDate(), userTimeZone).Date;

            var input = new HomeIndexViewModel(defaultFrom, defaultTo);

            return AssembleHomeIndexViewModel(input, userId);
        }      

        // TODO: Wow, this got ugly fast. Can this be simplified?
        public HomeIndexViewModel AssembleHomeIndexViewModel(HomeIndexViewModel homeIndexViewModelIn, string userId)
        {
            var indexViewModelResult = new HomeIndexViewModel(
                homeIndexViewModelIn.From, 
                homeIndexViewModelIn.To);
            indexViewModelResult.EntryGroups = new List<HomeIndexViewModelEntryGroup>();       

            var userTimeZone = _userSettingsService.Get(userId).TimeZone;            

            SetDefaultDatesWhenEmpty(indexViewModelResult, userTimeZone);            

            var userTimeZoneOffsetToTakeIntoAccount = 
                userTimeZone.BaseUtcOffset.TotalSeconds < 0 ? new TimeSpan() : userTimeZone.BaseUtcOffset;

            var entries = _entryService.GetEntries(
                indexViewModelResult.From.Value.ToUniversalTime().Subtract(userTimeZoneOffsetToTakeIntoAccount),
                indexViewModelResult.To.Value.ToUniversalTime(),
                userId);

            var start = indexViewModelResult.From.Value;
            var stop = indexViewModelResult.To.Value;            
           
            while (start < stop)
            {
                var startOfTheDay = start.Date;
                var endOfTheDay = start.Date.AddDays(1);
                
                foreach (var entry in entries)
                {
                    var entryCreatedOnLoc = TimeZoneInfo.ConvertTimeFromUtc(entry.CreatedOn, userTimeZone);
                    var entryStoppedOnLoc = (DateTime?)null;
                    if (entry.StoppedOn.HasValue)
                        entryStoppedOnLoc = TimeZoneInfo.ConvertTimeFromUtc(entry.StoppedOn.Value, userTimeZone);

                    var entryToAdd = new HomeIndexViewModelEntry();                    

                    if (entryStoppedOnLoc < startOfTheDay || 
                        entryCreatedOnLoc > endOfTheDay)
                        continue;

                    if (!entryStoppedOnLoc.HasValue)
                        entryStoppedOnLoc = DateTime.MaxValue;

                    // Entry spans full day
                    if (entryCreatedOnLoc <= startOfTheDay && entryStoppedOnLoc >= endOfTheDay)
                    {                       
                        entryToAdd.Start = startOfTheDay;
                        entryToAdd.End = endOfTheDay;
                        entryToAdd.Percentage = 100;
                    }
                    // Entry started before the current day
                    else if (entryCreatedOnLoc <= startOfTheDay)
                    {
                        var diff = entryStoppedOnLoc.Value.Subtract(startOfTheDay);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = startOfTheDay;
                        entryToAdd.End = entryStoppedOnLoc.Value;                     
                    }
                    // Entry stopped stopped after the current day
                    else if (entryStoppedOnLoc.Value >= endOfTheDay)
                    {
                        var diff = endOfTheDay.Subtract(entryCreatedOnLoc);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = entryCreatedOnLoc;
                        entryToAdd.End = endOfTheDay;                        
                    }
                    // Entry spans a part of the day
                    else
                    {
                        var diff = entryStoppedOnLoc.Value.Subtract(entryCreatedOnLoc);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = entryCreatedOnLoc;
                        entryToAdd.End = entryStoppedOnLoc.Value;                        
                    }

                    entryToAdd.Tag = entry.Tag == null ? string.Empty : entry.Tag.TitleCasedName;
                    entryToAdd.Color = entry.Tag == null ? string.Empty : entry.Tag.Color;
                    entryToAdd.Title = string.Format("{0} ({1}-{2}): {3}", 
                        new object[] { entryToAdd.Tag, entryToAdd.Start.ToShortTimeString(), entryToAdd.End.ToShortTimeString(), entry.Description });

                    if (indexViewModelResult.EntryGroups.Any(eg => eg.Date == startOfTheDay))
                    {
                        var oldEntryGroup = indexViewModelResult.EntryGroups.Where(eg => eg.Date == startOfTheDay).First();

                        var newEntryGroup = new HomeIndexViewModelEntryGroup();
                        newEntryGroup.Date = oldEntryGroup.Date;
                        newEntryGroup.Entries = oldEntryGroup.Entries;

                        newEntryGroup.Entries.Add(entryToAdd);

                        indexViewModelResult.EntryGroups.RemoveAll(eg => eg.Date == startOfTheDay);
                        indexViewModelResult.EntryGroups.Add(newEntryGroup);
                    }
                    else
                    {                  
                        var entryGroup = new HomeIndexViewModelEntryGroup();
                        entryGroup.Entries = new List<HomeIndexViewModelEntry>();
                        entryGroup.Date = startOfTheDay;                        
                        entryGroup.Entries.Add(entryToAdd);

                        indexViewModelResult.EntryGroups.Add(entryGroup);
                    }
                }

                start = start.AddDays(1);
            }

            return indexViewModelResult;
        }

        private void SetDefaultDatesWhenEmpty(HomeIndexViewModel indexViewModelResult, TimeZoneInfo userTimeZone)
        {
            if (!indexViewModelResult.From.HasValue)
                indexViewModelResult.From = TimeZoneInfo.ConvertTimeFromUtc(GetDefaultFromDate(), userTimeZone);
            if (!indexViewModelResult.To.HasValue)
                indexViewModelResult.To = TimeZoneInfo.ConvertTimeFromUtc(GetDefaultToDate(), userTimeZone);
        }       

        private DateTime GetDefaultFromDate()
        {
            return _timeService.GetNow().Date.AddDays(-14);
        }

        private DateTime GetDefaultToDate()
        {
            return _timeService.GetNow().Date.AddDays(1);
        }
    }
}
