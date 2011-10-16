using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class LocationRepository : ILocationRepository
    {
        private IDocaryContext _context;

        public LocationRepository(IDocaryContext context)
        {
            _context = context;
        }

        public IQueryable<Location> Get()
        {
            return _context.Locations;
        }
        
        public Location Add(Location location)
        {
            var addedLocation = _context.Locations.Add(location);

            _context.SaveChanges();

            return addedLocation;
        }

        public void Delete(int id)
        {
            var locationToDelete = _context.Locations.Where(l => l.Id == id).First();

            _context.Locations.Remove(locationToDelete);
            _context.SaveChanges();
        }
    }
}
