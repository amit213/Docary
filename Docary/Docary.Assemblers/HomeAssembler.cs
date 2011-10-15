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
        public IEntryService _entryService;

        public HomeAssembler(IEntryService entryService)
        {
            _entryService = entryService;
        }

        public HomeIndexViewModel AssembleHomeIndexViewModel()
        {
            var indexViewModel = new HomeIndexViewModel()
            {
                Entries = _entryService.GetEntries()
            };

            return indexViewModel;
        }
    }
}
