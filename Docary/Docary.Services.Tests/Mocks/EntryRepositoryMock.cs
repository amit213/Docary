using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Repositories;
using Docary.Models;

namespace Docary.Services.Tests.Mocks
{
    public class EntryRepositoryMock : IEntryRepository
    {
        private IList<Entry> _entries;
        private int _entryId = 0;

        private LocationRepositoryMock _locationRepositoryMock;
        private TagRepositoryMock _tagRepositoryMock;

        public EntryRepositoryMock()
        {
            _locationRepositoryMock = new LocationRepositoryMock();
            _tagRepositoryMock = new TagRepositoryMock();

            _entries = new List<Entry>();

            SeedEntries();
        }

        private void SeedEntries()
        {
            var entry = new Entry() {
                Id = 1,
                Location = _locationRepositoryMock.Get().Where(l => l.Id == 1).FirstOrDefault(),
                Tag = _tagRepositoryMock.Get().Where(t => t.Id == 1).FirstOrDefault(),
                UserId = "1",
                CreatedOn = DateTime.Now.AddHours(-1),
                StoppedOn = null
            };
         
            _entries.Add(entry);
        }

        public IEnumerable<Entry> Entries
        {
            get
            {
                return _entries;
            }
        }

        public void Delete(int id)
        {
            var entryToDelete = _entries.Where(e => e.Id == id).FirstOrDefault();

            _entries.Remove(entryToDelete);
        }

        public Entry Add(Entry item)
        {
            item.Id = _entryId++;                     
            
            _entries.Add(item);

            return item;
        }

        public IQueryable<Entry> Get()
        {
            return _entries.AsQueryable<Entry>();
        }

        public void Update(Entry item)
        {
            var entryToUpdate = _entries.Where(e => e.Id == item.Id).First();

            _entries.Remove(entryToUpdate);
            _entries.Add(item);           
        }
    }
}
