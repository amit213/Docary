﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Repositories
{
    public interface ILocationRepository
    {
        IQueryable<Location> GetLocations();
        int AddLocation(Location location);
    }
}
