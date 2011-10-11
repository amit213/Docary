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
        
        public void AddEntry(Entry entry)
        {
            _context.Entries.Add(entry);
            _context.SaveChanges();            
        }
    }
}
