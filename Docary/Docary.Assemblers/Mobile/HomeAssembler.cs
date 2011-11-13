using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels.Mobile;
using Docary.Services;

namespace Docary.ViewModelAssemblers.Mobile 
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
                entryGroup.Entries = group.ToList();

                indexViewModel.EntryGroups.Add(entryGroup);
            }           

            return indexViewModel;
        }
    }
}
