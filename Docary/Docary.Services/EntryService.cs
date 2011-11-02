﻿using System;
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

        public IEnumerable<Entry> GetEntries(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            return _entryRepository.Get().OrderByDescending(e => e.CreatedOn)
                                         .Where(e => e.UserId == userId && e.CreatedOn >= createdOnMin && e.CreatedOn <= createdOnMax)
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

            var location = _locationRepository.Find(entry.Location.Name, entry.UserId);
            var tag = _tagRepository.Find(entry.Tag.Name, entry.UserId);          

            entry.Location = location == null ? AddLocationBasedOn(entry) : location;
            entry.Tag = tag == null ? AddTagBasedOn(entry) : tag;
            entry.CreatedOn = _timeService.GetNow();            

            _entryRepository.Add(entry);
        }        

        private Entry GetLatestEntry(string userId)
        {
            return _entryRepository.Get()
                                   .Where(e => e.StoppedOn == null && e.UserId == userId)
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
