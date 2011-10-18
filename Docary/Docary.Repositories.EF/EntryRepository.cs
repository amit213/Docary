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
        
        public Entry Add(Entry entry)
        {
            var addedEntry = _context.Entries.Add(entry);
                       
            _context.SaveChanges();

            return addedEntry;                                
        }

        public void Delete(int id)
        {
            var entryToDelete = _context.Entries.Where(e => e.Id == id).First();

            _context.Entries.Remove(entryToDelete);
            _context.SaveChanges();
        }
    }
}
