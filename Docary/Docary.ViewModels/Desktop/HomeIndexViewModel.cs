using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;
using System.ComponentModel.DataAnnotations;

namespace Docary.ViewModels.Desktop
{
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel() 
        {
            EntryGroups = new List<HomeIndexViewModelEntryGroup>();
            Legenda = new List<HomeIndexViewModelLegendaTag>();
        }

        public HomeIndexViewModel(DateTime? from, DateTime? to)
        {
            EntryGroups = new List<HomeIndexViewModelEntryGroup>();
            Legenda = new List<HomeIndexViewModelLegendaTag>();
            From = from;
            To = to;
        }

        public List<HomeIndexViewModelEntryGroup> EntryGroups { get; set; }

        public bool HasEntries
        {
            get
            {
                return (EntryGroups != null && EntryGroups.Count > 0);
            }
        }

        [Required]        
        public DateTime? From { get; set; }

        [Required]       
        public DateTime? To { get; set; }

        public List<HomeIndexViewModelLegendaTag> Legenda { get; set; }
    }

    public class HomeIndexViewModelEntryGroup
    {
        public HomeIndexViewModelEntryGroup(DateTime date)
        {
            Entries = new List<HomeIndexViewModelEntry>();
            Date = date;
        }

        public DateTime Date { get; set; }
        public List<HomeIndexViewModelEntry> Entries { get; set; }
    }

    public class HomeIndexViewModelEntry
    {
        public HomeIndexViewModelEntry() { }

        public HomeIndexViewModelEntry(double percentage, DateTime start, DateTime end)
        {
            Percentage = percentage;
            Start = start;
            End = end;
        }

        public double Percentage { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Color { get; set; }
        public string Tag { get; set; }
        public string Title { get; set; }
    }

    public class HomeIndexViewModelLegendaTag
    {
        public HomeIndexViewModelLegendaTag(string name, string color)
        {
            Name = name;
            Color = color;
        }

        public string Name { get; set; }
        public string Color { get; set; }
    }
}
