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

        public IQueryable<Location> GetLocations()
        {
            return _context.Locations;
        }
        
        public int AddLocation(Location location)
        {
            var locationId = _context.Locations.Add(location).Id;

            _context.SaveChanges();

            return locationId;
        }
    }
}
