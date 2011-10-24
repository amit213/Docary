using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Repositories;
using Docary.Models;

namespace Docary.Services.Tests.Mocks
{
    public class LocationRepositoryMock : ILocationRepository
    {
        private IList<Location> _items;
        private int _id = 0;

        public LocationRepositoryMock()
        {
            _items = new List<Location>();
            _items.Add(new Location() { Id = 1, Name = "TestLocation", UserId = "1" });
        }

        public IList<Location> Locations
        {
            get
            {
                return _items;
            }
        }

        public Location Add(Location item)
        {
            item.Id = _id++;                     
            
            _items.Add(item);

            return item;
        }

        public IQueryable<Location> Get()
        {
            return _items.AsQueryable<Location>();
        }
    }
}
