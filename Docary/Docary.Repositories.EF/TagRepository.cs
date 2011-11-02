using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class TagRepository : ITagRepository
    {
        private DocaryContext _context;

        public TagRepository(DocaryContext context)
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

        public EntryTag Find(string name, string userId)
        {
            return _context.Tags.Where(a => a.Name == name && a.UserId == userId).FirstOrDefault();
        }
    }
}
