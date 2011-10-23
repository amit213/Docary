using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Docary.Models;
using System.Data.Entity.Validation;

namespace Docary.Repositories.EF
{
    public class EntryRepository : IEntryRepository
    {
        private DocaryContext _context;

        public EntryRepository(DocaryContext context)
        {
            _context = context;
        }

        public IQueryable<Entry> Get()
        {
            return _context.Entries.Include(e => e.Tag).Include(e => e.Location); ;
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
