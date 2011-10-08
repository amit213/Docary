using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public interface IDocaryContext
    {
        IDbSet<Activity> Activities { get; set; }
        IDbSet<Entry> Entries { get; set; }
        IDbSet<Location> Locations { get; set; }
    }
}
