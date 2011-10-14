using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class EntryRepository : IEntryRepository
    {
        private IDocaryContext _context;

        public EntryRepository(IDocaryContext context)
        {
            _context = context;
        }

        public IQueryable<Entry> Get()
        {
            return _context.Entries;
        }
        
        public int Add(Entry entry)
        {
            //TODO: Can't EF do this for me?
            entry.CreatedOn = DateTime.Now;

            var addedEntry =_context.Entries.Add(entry).Id;                      

            _context.SaveChanges();

            return addedEntry;
        }
    }
}
