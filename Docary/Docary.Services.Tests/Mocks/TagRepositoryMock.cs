using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Repositories;
using Docary.Models;

namespace Docary.Services.Tests.Mocks
{
    public class TagRepositoryMock : ITagRepository
    {
        private IList<EntryTag> _items;
        private int _id = 0;

        public TagRepositoryMock()
        {
            _items = new List<EntryTag>();
            _items.Add(new EntryTag() { Id = 1, Name = "TestTag", UserId = "1" });
        }

        public IEnumerable<EntryTag> Tags
        {
            get
            {
                return _items;
            }
        }

        public EntryTag Add(EntryTag item)
        {
            item.Id = _id++;                     
            
            _items.Add(item);

            return item;
        }

        public IQueryable<EntryTag> Get()
        {
            return _items.AsQueryable<EntryTag>();
        }

        public EntryTag Find(string name, string userId)
        {
            return _items.Where(i => i.Name == name && i.UserId == userId).FirstOrDefault();
        }
    }
}
