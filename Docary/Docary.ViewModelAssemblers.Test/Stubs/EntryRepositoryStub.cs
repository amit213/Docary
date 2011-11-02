using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;
using Docary.Repositories;

namespace Docary.ViewModelAssemblers.Test.Stubs
{
    public class EntryRepositoryStub : IEntryRepository
    {
        private IEnumerable<Entry> _entries;

        public void Seed(IEnumerable<Entry> entries)
        {
            _entries = entries;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Entry Add(Entry item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Entry> Get()
        {
            return _entries.AsQueryable();
        }

        public void Update(Entry item)
        {
            throw new NotImplementedException();
        }
    }
}
