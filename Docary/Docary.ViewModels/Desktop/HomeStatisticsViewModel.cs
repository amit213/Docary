using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.ViewModels.Desktop
{
    public class HomeStatisticsViewModel
    {
        public HomeStatisticsViewModel() { }

        public HomeStatisticsViewModel(DateTime? from, DateTime? to)
        {
            From = from;
            To = to;
        }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

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
        public string PercentageAsString { get; set; }
        public TimeSpan Time { get; set; }
        public string TagName { get; set; }
        public string TagColor { get; set; }
    }
}
