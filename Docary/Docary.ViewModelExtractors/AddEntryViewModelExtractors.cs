using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;
using Docary.ViewModels;

namespace Docary.ViewModelExtractors
{
    public static class AddEntryViewModelExtractors
    {
        public static Entry ExtractEntry(this AddEntryViewModel addEntryViewModel) 
        {
            var entry = new Entry();

            entry.Activity = new Activity();
            entry.Activity.Name = addEntryViewModel.ActivityName;
            entry.Location = new Location();
            entry.Location.Name = addEntryViewModel.LocationName;
            entry.Meta = addEntryViewModel.Meta;

            return entry;
        }
    }
}
