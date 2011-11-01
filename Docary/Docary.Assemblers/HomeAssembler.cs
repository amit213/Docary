using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels;
using Docary.Services;

namespace Docary.ViewModelAssemblers 
{
    public class HomeAssembler : IHomeAssembler
    {
        private IEntryService _entryService;

        public HomeAssembler(IEntryService entryService)
        {
            _entryService = entryService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel(DateTime from, DateTime to, string userId)
        {
            var indexViewModel = new HomeIndexViewModel();

            var entries = _entryService.GetEntries(from, to, userId);
            var groups = entries.GroupBy(e => e.CreatedOn.Date);

            indexViewModel.EntryGroups = new List<HomeIndexViewModelEntryGroup>();

            foreach (var group in groups)
            {
                var entryGroup = new HomeIndexViewModelEntryGroup();
                entryGroup.Date = group.First().CreatedOn.Date;
                entryGroup.Entries = group.ToList();

                indexViewModel.EntryGroups.Add(entryGroup);
            }           

            return indexViewModel;
        }
    }
}
