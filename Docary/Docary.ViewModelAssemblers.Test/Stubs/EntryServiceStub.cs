using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Services;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Test.Stubs
{
    public class EntryServiceStub : IEntryService
    {
        private IEnumerable<Entry> _entries;

        public void Seed(IEnumerable<Entry> entries)
        {
            _entries = entries;
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            return _entries.Where(e => e.UserId == user);
        }

        public void AddEntry(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntry(int id)
        {
            throw new NotImplementedException();
        }
    }
}
