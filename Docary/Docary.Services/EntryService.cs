using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Text;
using System.Linq;

using Docary.Models;
using Docary.Repositories;
using Docary.Repositories.EF;

namespace Docary.Services
{
    public class EntryService : IEntryService
    {
        private IEntryRepository _entryRepository;
        private ILocationRepository _locationRepository;
        private ITagRepository _tagRepository;
        private ITimelineColorService _timelineColorService;
        private ITimeService _timeService;
        private DocaryContext _context;

        public EntryService(IEntryRepository entryRepository, 
                            ILocationRepository locationRepository, 
                            ITagRepository tagRepository,
                            ITimelineColorService timelineColorService,
                            ITimeService timeService,
                            DocaryContext context)
        {
            _entryRepository = entryRepository;
            _locationRepository = locationRepository;
            _tagRepository = tagRepository;
            _timelineColorService = timelineColorService;
            _timeService = timeService;
            _context = context;
        }

        public IEnumerable<Entry> GetEntries(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            return _entryRepository.Get(createdOnMin, createdOnMax, userId);
        }

        public Entry GetLatestEntry(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            return _entryRepository.GetLatestEntry(userId);
        }

        public Entry GetFirstRealEntry(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException("userId");

            return _entryRepository.GetFirstRealEntry(userId);
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("Entry");
            if (string.IsNullOrEmpty(entry.UserId))
                throw new ArgumentNullException("UserId");

            var now = _timeService.GetNow();

            if (_entryRepository.IsEmpty(entry.UserId))            
                AddFirstOffTheGridEntry(entry.UserId, now);            

            var location = _locationRepository.Find(entry.Location.Name, entry.UserId);
            var tag = _tagRepository.Find(entry.Tag.Name, entry.UserId);           
            var latestEntry = _entryRepository.GetLatestEntry(entry.UserId);

            if (latestEntry != null) 
            {
                latestEntry.StoppedOn = now;
                _entryRepository.Update(latestEntry);
            }                                  

            entry.Location = location == null ? AddLocationBasedOnEntry(entry) : location;
            entry.Tag = tag == null ? AddTagBasedOnEntry(entry) : tag;
            entry.CreatedOn = _timeService.GetNow();            

            _entryRepository.Add(entry);

            _context.SaveChanges();
        }

        public int GetNumberOfEntries(string userId)
        {
            return _entryRepository.Count(userId);
        }

        private Location AddLocationBasedOnEntry(Entry entry)
        {
            var location = new Location(entry.Location.Name, entry.UserId);

            return _locationRepository.Add(location);
        }

        private void AddFirstOffTheGridEntry(string userId, DateTime stoppedOn)
        {
            var offTheGridTag = AddOffTheGridTag(userId);
            var offTheGridLocation = AddOffTheGridLocation(userId);

            var offTheGridEntry = new Entry()
            {
                CreatedOn = new DateTime(2010, 1, 1),
                Description = "Off the grid",
                Location = offTheGridLocation,
                StoppedOn = stoppedOn,
                Tag = offTheGridTag,
                UserId = userId
            };

            _entryRepository.Add(offTheGridEntry);
        }

        private Location AddOffTheGridLocation(string userId)
        {
            return _locationRepository.Add(new Location("Off the grid", userId));
        }

        private EntryTag AddOffTheGridTag(string userId)
        {
            return _tagRepository.Add(new EntryTag("Off the grid", "#FFF", userId));
        }

        private EntryTag AddTagBasedOnEntry(Entry entry)
        {
            var randomColor = _timelineColorService.GetRandom().Value;

            var tag = new EntryTag(entry.Tag.Name, randomColor, entry.UserId);

            return _tagRepository.Add(tag);
        }       
    }
}
