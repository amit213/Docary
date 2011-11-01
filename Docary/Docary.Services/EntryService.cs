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
        private ITimeService _timeService;

        public EntryService(IEntryRepository entryRepository, 
                            ILocationRepository locationRepository, 
                            ITagRepository tagRepository,
                            ITimeService timeService)
        {
            _entryRepository = entryRepository;
            _locationRepository = locationRepository;
            _tagRepository = tagRepository;
            _timeService = timeService;
        }        

        public IEnumerable<Entry> GetEntries(string user)
        {
            return _entryRepository.Get().Where(e => e.UserId == user)
                                         .OrderByDescending(e => e.CreatedOn)
                                         .ToList();
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("Entry");
            if (string.IsNullOrEmpty(entry.UserId))
                throw new ArgumentNullException("UserId");                                   

            var latestEntry = GetLatestEntry(entry.UserId);

            if (latestEntry != null)
            {
                latestEntry.StoppedOn = _timeService.GetNow();

                _entryRepository.Update(latestEntry);
            }

            var location = GetLocation(entry.Location.Name, entry.UserId);
            var tag = GetTag(entry.Tag.Name, entry.UserId);          

            entry.Location = location == null ? AddLocationBasedOn(entry) : location;
            entry.Tag = tag == null ? AddTagBasedOn(entry) : tag;
            entry.CreatedOn = _timeService.GetNow();            

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

        private Location GetLocation(string name, string userId)
        {
            return _locationRepository
                        .Get()
                        .Where(l => l.Name == name && l.UserId == userId)
                        .FirstOrDefault();
        }

        private EntryTag GetTag(string name, string userId)
        {
            return _tagRepository
                        .Get()
                        .Where(a => a.Name == name && a.UserId == userId)
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
