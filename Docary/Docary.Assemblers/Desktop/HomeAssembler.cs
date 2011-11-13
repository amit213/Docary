﻿using System;
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

        public HomeAssembler(IEntryService entryService)
        {
            _entryService = entryService;
        }

        // TODO: Wow, clean this up
        public HomeIndexViewModel AssembleHomeIndexViewModel(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            var indexViewModel = new HomeIndexViewModel();

            var sourceEntries = _entryService.GetEntries(createdOnMin, createdOnMax, userId);

            indexViewModel.EntryGroups = new List<HomeIndexViewModelEntryGroup>();            

            var start = createdOnMin;
            var stop = createdOnMax;

            var entryToAdd = new HomeIndexViewModelEntry();

            while (start < stop)
            {
                var startOfTheDay = start.Date;
                var endOfTheDay = start.Date.AddDays(1);

                foreach (var sourceEntry in sourceEntries)
                {
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
                        var diffPercent = diff.TotalMinutes / (24 * 60);

                        entryToAdd.Percentage = Convert.ToInt32(diffPercent * 100);
                        entryToAdd.Start = startOfTheDay;
                        entryToAdd.End = sourceEntry.StoppedOn.Value;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }
                    else if (sourceEntry.StoppedOn.Value >= endOfTheDay)
                    {
                        var diff = endOfTheDay.Subtract(sourceEntry.CreatedOn);
                        var diffPercent = diff.TotalMinutes / (24 * 60);

                        entryToAdd.Percentage = Convert.ToInt32(diffPercent * 100);
                        entryToAdd.Start = sourceEntry.CreatedOn;
                        entryToAdd.End = endOfTheDay;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }
                    else
                    {
                        var diff = sourceEntry.StoppedOn.Value.Subtract(sourceEntry.CreatedOn);
                        var diffPercent = diff.TotalMinutes / (24 * 60);

                        entryToAdd.Percentage = Convert.ToInt32(diffPercent * 100);
                        entryToAdd.Start = sourceEntry.CreatedOn;
                        entryToAdd.End = sourceEntry.StoppedOn.Value;
                        entryToAdd.Color = sourceEntry.Tag == null ? string.Empty : sourceEntry.Tag.Color;
                    }

                    if (indexViewModel.EntryGroups.Any(eg => eg.Date == startOfTheDay))
                    {
                        var oldEntryGroup = indexViewModel.EntryGroups.Where(eg => eg.Date == startOfTheDay).First();

                        var newEntryGroup = new HomeIndexViewModelEntryGroup();
                        newEntryGroup.Date = oldEntryGroup.Date;
                        newEntryGroup.Entries = oldEntryGroup.Entries;
                        newEntryGroup.Entries.Add(entryToAdd);

                        indexViewModel.EntryGroups.Remove(oldEntryGroup);
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
    }
}