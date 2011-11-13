using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Services;

using Docary.ViewModelAssemblers.Desktop;
using Docary.ViewModels.Desktop;

namespace Docary.ViewModelAssemblers.Desktop
{
    public class HomeAssembler : IHomeAssembler
    {
        private IEntryService _entryService;

        public HomeAssembler(IEntryService entryService)
        {
            _entryService = entryService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            var indexViewModel = new HomeIndexViewModel();

            var entries = _entryService.GetEntries(createdOnMin, createdOnMax, userId);
            var groups = entries.GroupBy(e => e.CreatedOn.Date);

            indexViewModel.EntryGroups = new List<HomeIndexViewModelEntryGroup>();

            foreach (var group in groups)
            {
                var entryGroup = new HomeIndexViewModelEntryGroup();
                entryGroup.Date = group.First().CreatedOn.Date;
                entryGroup.Entries = new List<HomeIndexViewModelEntry>();

                foreach(var entry in group.ToList()) {
                    var homeIndexViewModelEntry = new HomeIndexViewModelEntry();

                    if (!entry.StoppedOn.HasValue)
                        entry.StoppedOn = entry.CreatedOn.Date.AddDays(1);

                    var diff = entry.StoppedOn.Value.Subtract(entry.CreatedOn);
                    var diffPercent = diff.TotalMinutes / (24 * 60);

                    homeIndexViewModelEntry.Percentage = Convert.ToInt32(diffPercent * 100);
                    homeIndexViewModelEntry.Color = entry.Tag == null ? string.Empty : entry.Tag.Color;

                    entryGroup.Entries.Add(homeIndexViewModelEntry);                  
                }                

                indexViewModel.EntryGroups.Add(entryGroup);
            }

            return indexViewModel;
        }
    }
}
