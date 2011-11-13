using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.ViewModels.Desktop;

namespace Docary.ViewModelAssemblers.Desktop
{
    public interface IHomeAssembler
    {
        HomeIndexViewModel AssembleHomeIndexViewModel(DateTime createdOnMin, DateTime createdOnMax, string userId);
    }
}
