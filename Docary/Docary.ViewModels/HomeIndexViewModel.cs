using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Entry> Entries { get; set; }
    }
}
