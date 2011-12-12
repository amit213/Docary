using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Repositories
{
    public interface ILocationRepository : IBaseRepository<Location>
    {
        Location Find(string name, string userId);
    }
}
