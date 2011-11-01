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

        public IEnumerable<Entry> GetEntries(DateTime from, DateTime to, string user)
        {
            return new List<Entry>();
        }

        public void AddEntry(Entry entry) { }
        public void DeleteEntry(int id) { } 
    }
}
