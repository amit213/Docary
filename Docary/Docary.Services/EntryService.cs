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
        private IEntryRepository _entryRepository;
        private ILocationRepository _locationRepository;
        private ITagRepository _tagRepository;

        public EntryService(IEntryRepository entryRepository, ILocationRepository locationRepository, ITagRepository tagRepository)
        {
            _entryRepository = entryRepository;
            _locationRepository = locationRepository;
            _tagRepository = tagRepository;
        }

        public IEnumerable<Entry> GetEntries()
        {
            return _entryRepository.Get().OrderByDescending(e => e.CreatedOn).ToList();
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("Entry");
                        
            var now = DateTime.Now;            

            var latestEntry = GetLatestEntry(entry.UserId);

            if (latestEntry != null)
            {
                latestEntry.StoppedOn = now;

                //Iew, make this updateable
                _entryRepository.Delete(latestEntry.Id);
                _entryRepository.Add(latestEntry);
            }

            var location = ResolveLocation(entry.Location.Name);
            var tag = ResolveTag(entry.Tag.Name);

            entry.Location = location == null ? AddLocationBasedOn(entry) : location;
            entry.Tag = tag == null ? AddTagBasedOn(entry) : tag;
            entry.CreatedOn = now;            

            _entryRepository.Add(entry);
        }

        public void DeleteEntry(int id)
        {
            _entryRepository.Delete(id);
        }

        private Entry GetLatestEntry(string userId)
        {
            return _entryRepository.Get().Where(e => e.StoppedOn == null && e.UserId == userId).FirstOrDefault();
        }

        private Location ResolveLocation(string name)
        {
            return _locationRepository
                        .Get()
                        .Where(l => l.Name == name)
                        .FirstOrDefault();
        }

        private EntryTag ResolveTag(string name)
        {
            return _tagRepository
                        .Get()
                        .Where(a => a.Name == name)
                        .FirstOrDefault();
        }

        private Location AddLocationBasedOn(Entry entry)
        {
            var location = new Location()
            {
                Name = entry.Location.Name,
                UserId = entry.UserId
            };

            return _locationRepository.Add(location);
        }

        private EntryTag AddTagBasedOn(Entry entry)
        {
            var tag = new EntryTag()
            {
                Name = entry.Tag.Name,
                UserId = entry.UserId
            };

            return _tagRepository.Add(tag);
        }
    }
}
