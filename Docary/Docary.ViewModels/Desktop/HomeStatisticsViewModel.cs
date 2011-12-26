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

        public int NumberOfEntries { get; set; }

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
        public double Percentage { get; set; }
        public TimeSpan Time { get; set; }
        public EntryTag Tag { get; set; }
    }
}
