using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class LocationRepository : RepositoryBase<Location>, ILocationRepository
    {       
        public LocationRepository(DocaryContext context) : base(context) { }       

        public Location Find(string name, string userId)
        {
            return Context.Locations.Where(l => l.Name == name && l.UserId == userId).FirstOrDefault();
        }
    }
}
