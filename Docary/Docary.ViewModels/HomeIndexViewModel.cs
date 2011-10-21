using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<HomeIndexViewModelEntryGroup> EntryGroups { get; set; }
    }

    public class HomeIndexViewModelEntryGroup
    {
        public DateTime Date { get; set; }
        public List<Entry> Entries { get; set; }       
    }
}
