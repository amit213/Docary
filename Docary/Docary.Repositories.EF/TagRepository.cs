using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class TagRepository : ITagRepository
    {
        private IDocaryContext _context;

        public TagRepository(IDocaryContext context)
        {
            _context = context;
        }

        public IQueryable<EntryTag> Get()
        {
            return _context.Tags;
        }
        
        public EntryTag Add(EntryTag tag)
        {
            var addedTag = _context.Tags.Add(tag);

            _context.SaveChanges();

            return addedTag;
        }

        public void Delete(int id)
        {
            var tagToDelete = _context.Tags.Where(a => a.Id == id).First();

            _context.Tags.Remove(tagToDelete);
            _context.SaveChanges();
        }
    }
}
