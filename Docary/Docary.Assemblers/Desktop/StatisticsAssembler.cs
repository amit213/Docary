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

            homeStatisticsViewModel.LatestEntry = _entryService.GetLatestEntry(userId).CreatedOn;
            homeStatisticsViewModel.FirstEntry = _entryService.GetFirstRealEntry(userId).CreatedOn;

            return homeStatisticsViewModel;
        }
    }
}
