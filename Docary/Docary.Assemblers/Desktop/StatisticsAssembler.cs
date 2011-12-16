using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.ViewModels.Desktop;
using Docary.Services;

namespace Docary.ViewModelAssemblers.Desktop
{
    public class StatisticsAssembler : IStatisticsAssembler
    {
        private IEntryService _entryService;

        public StatisticsAssembler(IEntryService entryService)
        {
            _entryService = entryService;
        }

        public HomeStatisticsViewModel AssembleHomeStatisticsViewModel(string userId)
        {
            var homeStatisticsViewModel = new HomeStatisticsViewModel();

            var latestEntry = _entryService.GetLatestEntry(userId);
            var firstEntry = _entryService.GetFirstRealEntry(userId);

            homeStatisticsViewModel.LatestEntry = latestEntry == null ? (DateTime?)null : latestEntry.CreatedOn;
            homeStatisticsViewModel.FirstEntry = firstEntry == null ? (DateTime?)null : firstEntry.CreatedOn;
            homeStatisticsViewModel.NumberOfEntries = _entryService.GetNumberOfEntries(userId);

            return homeStatisticsViewModel;
        }
    }
}
