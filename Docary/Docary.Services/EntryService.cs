using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;
using System.Linq;

using Docary.Models;
using Docary.Repositories;

namespace Docary.Services
{
    public class EntryService : IEntryService
    {
        private IEntryRepository _repository;

        public EntryService(IEntryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Entry> GetEntries()
        {
            return _repository.GetEntries().ToList();
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            throw new NotImplementedException();
        }
    }
}
