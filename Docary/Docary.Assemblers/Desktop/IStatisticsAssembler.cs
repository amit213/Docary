using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.ViewModels.Desktop;

namespace Docary.ViewModelAssemblers.Desktop
{
    public interface IStatisticsAssembler
    {
        HomeStatisticsViewModel AssembleHomeStatisticsViewModel(string userId);

        HomeStatisticsViewModel AssembleHomeStatisticsViewModel(string userId, DateTime from, DateTime to);

        HomeStatisticsViewModel AssembleHomeStatisticsViewModel(HomeStatisticsViewModel statisticsViewmodel, string userId);
    }
}
