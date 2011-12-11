using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Services;

using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Desktop
{
    public class HomeAssembler : IHomeAssembler
    {
        private IEntryService _entryService;
        private ITimeService _timeService;

        public HomeAssembler(IEntryService entryService, ITimeService timeService)
        {
            _entryService = entryService;
            _timeService = timeService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(string userId)
        {
            var input = new HomeIndexViewModel(GetDefaultFromDate(), GetDefaultToDate());

            return AssembleHomeIndexViewModel(input, userId);
        }      

        // TODO: Wow, this got ugly fast. Can this be simplified?
        public HomeIndexViewModel AssembleHomeIndexViewModel(HomeIndexViewModel homeIndexViewModelIn, string userId)
        {
            var indexViewModel = new HomeIndexViewModel(homeIndexViewModelIn.From, homeIndexViewModelIn.To);
            
            EnsureDatesAreNotNull(indexViewModel);

            var start = indexViewModel.From.Value;
            var stop = indexViewModel.To.Value;           
            var sourceEntries = _entryService.GetEntries(indexViewModel.From.Value, indexViewModel.To.Value, userId);

            indexViewModel.EntryGroups = new List<HomeIndexViewModelEntryGroup>();
       
            while (start < stop)
            {
                var startOfTheDay = start.Date;
                var endOfTheDay = start.Date.AddDays(1);
                
                foreach (var sourceEntry in sourceEntries)
                {
                    var entryToAdd = new HomeIndexViewModelEntry();

                    entryToAdd.Tag = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Name;

                    if (sourceEntry.StoppedOn < startOfTheDay)
                        continue;
                    if (sourceEntry.CreatedOn > endOfTheDay)
                        continue;

                    if (!sourceEntry.StoppedOn.HasValue)
                        sourceEntry.StoppedOn = DateTime.MaxValue;

                    if (sourceEntry.CreatedOn <= startOfTheDay && sourceEntry.StoppedOn >= endOfTheDay)
                    {                        
                        entryToAdd.Color = sourceEntry.Tag == null ? "" : sourceEntry.Tag.Color;
                        entryToAdd.Start = startOfTheDay;
                        entryToAdd.End = endOfTheDay;
                        entryToAdd.Percentage = 100;
                    }
                    else if (sourceEntry.CreatedOn <= startOfTheDay)
                    {
                        var diff = sourceEntry.StoppedOn.Value.Subtract(startOfTheDay);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = startOfTheDay;
                        entryToAdd.End = sourceEntry.StoppedOn.Value;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }
                    else if (sourceEntry.StoppedOn.Value >= endOfTheDay)
                    {
                        var diff = endOfTheDay.Subtract(sourceEntry.CreatedOn);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = sourceEntry.CreatedOn;
                        entryToAdd.End = endOfTheDay;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }
                    else
                    {
                        var diff = sourceEntry.StoppedOn.Value.Subtract(sourceEntry.CreatedOn);
                        var diffPercent = diff.TotalSeconds / (24 * 3600);

                        entryToAdd.Percentage = diffPercent * 100;
                        entryToAdd.Start = sourceEntry.CreatedOn;
                        entryToAdd.End = sourceEntry.StoppedOn.Value;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }

                    entryToAdd.Title = string.Format("{0} ({1}-{2})", 
                        new object[] { entryToAdd.Tag, entryToAdd.Start.ToShortTimeString(), entryToAdd.End.ToShortTimeString() });

                    if (indexViewModel.EntryGroups.Any(eg => eg.Date == startOfTheDay))
                    {
                        var oldEntryGroup = indexViewModel.EntryGroups.Where(eg => eg.Date == startOfTheDay).First();

                        var newEntryGroup = new HomeIndexViewModelEntryGroup();
                        newEntryGroup.Date = oldEntryGroup.Date;
                        newEntryGroup.Entries = oldEntryGroup.Entries;

                        newEntryGroup.Entries.Add(entryToAdd);

                        indexViewModel.EntryGroups.RemoveAll(eg => eg.Date == startOfTheDay);
                        indexViewModel.EntryGroups.Add(newEntryGroup);
                    }
                    else
                    {                  
                        var entryGroup = new HomeIndexViewModelEntryGroup();
                        entryGroup.Entries = new List<HomeIndexViewModelEntry>();
                        entryGroup.Date = startOfTheDay;                        
                        entryGroup.Entries.Add(entryToAdd);

                        indexViewModel.EntryGroups.Add(entryGroup);
                    }
                }

                start = start.AddDays(1);
            }

            return indexViewModel;
        }

        private void EnsureDatesAreNotNull(HomeIndexViewModel indexViewModel)
        {
            if (!indexViewModel.From.HasValue)
                indexViewModel.From = GetDefaultFromDate();
            if (!indexViewModel.To.HasValue)
                indexViewModel.To = GetDefaultToDate();
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
