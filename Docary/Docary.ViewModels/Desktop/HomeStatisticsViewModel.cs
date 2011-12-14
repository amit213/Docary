using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.ViewModels.Desktop
{
    public class HomeStatisticsViewModel
    {
        public HomeStatisticsPerTagViewModel PerTag { get; set; }

        public DateTime? FirstEntry { get; set; }

        public DateTime? LatestEntry { get; set; }

        public bool HasFirstEntry
        {
            get
            {
                return FirstEntry.HasValue;
            }
        }

        public bool HasLatestEntry
        {
            get
            {
                return LatestEntry.HasValue;
            }
        }
    }

    public class HomeStatisticsPerTagViewModel
    {
        public IEnumerable<HomeStatisticsPerTagItem> Items { get; set; }
    }

    public class HomeStatisticsPerTagItem
    {
        public int Percentage { get; set; }
        public int Hours { get; set; }
        public EntryTag Tag { get; set; }
    }
}
