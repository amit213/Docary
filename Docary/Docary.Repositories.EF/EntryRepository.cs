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

        public IQueryable<Entry> GetEntries()
        {
            return _context.Entries;
        }
        
        public int AddEntry(Entry entry)
        {        
            var entryId =_context.Entries.Add(entry).Id;

            //TODO: Can't EF do this for me?
            entry.CreatedOn = DateTime.Now;

            _context.SaveChanges();

            return entryId;
        }
    }
}
