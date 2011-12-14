using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Services;
using Docary.Models;

namespace Docary.Tests.Stubs
{
    public class EntryServiceEmptyStub : IEntryService
    {
        public IEnumerable<Entry> GetEntries()
        {
            return new List<Entry>();
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            return new List<Entry>();
        }

        public IEnumerable<Entry> GetEntries(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            return new List<Entry>();
        }

        public void AddEntry(Entry entry) { }
        public void DeleteEntry(int id) { }
        
        public Entry GetLatestEntry(string userId)
        {
            return new Entry();
        }

        public Entry GetFirstRealEntry(string userId)
        {
            return new Entry();
        }
    }
}
