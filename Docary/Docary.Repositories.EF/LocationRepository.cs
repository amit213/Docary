using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class LocationRepository : ILocationRepository
    {
        private DocaryContext _context;

        public LocationRepository(DocaryContext context)
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
    }
}
