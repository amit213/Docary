using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class LocationRepository : RepositoryBase, ILocationRepository
    {       
        public LocationRepository(DocaryContext context) : base(context) { }

        public IQueryable<Location> Get()
        {
            return Context.Locations;
        }
        
        public Location Add(Location location)
        {
            var addedLocation = Context.Locations.Add(location);

            Context.SaveChanges();

            return addedLocation;
        }

        public Location Find(string name, string userId)
        {
            return Context.Locations.Where(l => l.Name == name && l.UserId == userId).FirstOrDefault();
        }
    }
}
