using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.ViewModels.Desktop;

namespace Docary.ViewModelAssemblers.Desktop
{
    public interface IHomeAssembler
    {
        HomeIndexViewModel AssembleHomeIndexViewModel(string userId);

        HomeIndexViewModel AssembleHomeIndexViewModel(string userId, DateTime from, DateTime to);

        HomeIndexViewModel AssembleHomeIndexViewModel(HomeIndexViewModel homeIndexViewModel, string userId);
    }
}
