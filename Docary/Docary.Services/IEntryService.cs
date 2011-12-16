using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Services
{
    public interface IEntryService
    {      
        IEnumerable<Entry> GetEntries(DateTime createdOnMin, DateTime createdOnMax, string userId);

        Entry GetLatestEntry(string userId);

        Entry GetFirstRealEntry(string userId);

        void AddEntry(Entry entry);

        int GetNumberOfEntries(string userId);
    }
}
