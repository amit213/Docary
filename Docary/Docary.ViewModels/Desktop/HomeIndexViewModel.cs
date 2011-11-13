﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.ViewModels.Desktop
{
    public class HomeIndexViewModel
    {
        public List<HomeIndexViewModelEntryGroup> EntryGroups { get; set; }

        public bool HasEntries
        {
            get
            {
                return (EntryGroups != null && EntryGroups.Count > 0);
            }
        }
    }

    public class HomeIndexViewModelEntryGroup
    {
        public DateTime Date { get; set; }
        public List<HomeIndexViewModelEntry> Entries { get; set; }
    }

    public class HomeIndexViewModelEntry
    {
        public int Percentage { get; set; }
        public string Color { get; set; }
    }
}
