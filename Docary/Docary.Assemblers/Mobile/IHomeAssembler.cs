using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels.Mobile;

namespace Docary.ViewModelAssemblers.Mobile
{
    public interface IHomeAssembler
    {
        HomeIndexViewModel AssembleHomeIndexViewModel(DateTime createdOnMin, DateTime createdOnMax, string userId);
    }
}
