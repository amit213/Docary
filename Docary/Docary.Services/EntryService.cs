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
        private IActivityRepository _activityRepository;

        public EntryService(IEntryRepository entryRepository, ILocationRepository locationRepository, IActivityRepository activityRepository)
        {
            _entryRepository = entryRepository;
            _locationRepository = locationRepository;
            _activityRepository = activityRepository;
        }

        public IEnumerable<Entry> GetEntries()
        {
            return _entryRepository.Get().ToList();
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("Entry");

            entry.CreatedOn = DateTime.Now;
            entry.StoppedOn = DateTime.MaxValue;

            var location = ResolveLocation(entry.Location.Name);
            var activity = ResolveActivity(entry.Activity.Name);

            entry.Location = location == null ? AddLocationBasedOn(entry) : location;
            entry.Activity = activity == null ? AddActivityBasedOn(entry) : activity;

            _entryRepository.Add(entry);
        }

        public void DeleteEntry(int id)
        {
            _entryRepository.Delete(id);
        }

        private Location ResolveLocation(string name)
        {
            return _locationRepository
                        .Get()
                        .Where(l => l.Name == name)
                        .FirstOrDefault();
        }

        private Activity ResolveActivity(string name)
        {
            return _activityRepository
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

        private Activity AddActivityBasedOn(Entry entry)
        {
            var activity = new Activity()
            {
                Name = entry.Activity.Name,
                UserId = entry.UserId
            };

            return _activityRepository.Add(activity);
        }
    }
}
