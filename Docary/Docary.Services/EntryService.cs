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

            var location = TryToResolveLocation(entry.Location.Name);
            var activity = TryToResolveActivity(entry.Activity.Name);

            entry.LocationId = location == null ? AddLocation(entry.Location).Id : location.Id;
            entry.ActivityId = activity == null ? AddActivity(entry.Activity).Id : activity.Id;

            _entryRepository.Add(entry);
        }

        public void DeleteEntry(int id)
        {
            _entryRepository.Delete(id);
        }

        private Location TryToResolveLocation(string name)
        {
            return _locationRepository.Get().Where(l => l.Name == name).FirstOrDefault();
        }

        private Activity TryToResolveActivity(string name)
        {
            return _activityRepository.Get().Where(a => a.Name == name).FirstOrDefault();
        }

        private Location AddLocation(Location location)
        {
            return _locationRepository.Add(location);
        }

        private Activity AddActivity(Activity activity)
        {
           return _activityRepository.Add(activity);
        }
    }
}
