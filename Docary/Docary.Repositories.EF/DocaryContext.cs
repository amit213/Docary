﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class DocaryContext : DbContext, IDocaryContext
    {
        public DocaryContext() : base("Docary") { }

        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<Entry> Entries { get; set; }
        public IDbSet<Location> Locations { get; set; }
    }
}
