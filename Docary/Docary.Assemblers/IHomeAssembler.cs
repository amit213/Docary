using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.ViewModels;

namespace Docary.Assemblers
{
    public interface IHomeAssembler
    {
        HomeIndexViewModel AssembleHomeIndexViewModel();
    }
}
