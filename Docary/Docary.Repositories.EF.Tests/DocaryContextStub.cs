﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Docary.Models;
using Docary.Repositories.EF.Tests.Fakes;

namespace Docary.Repositories.EF.Tests
{
    public class DocaryContextStub : IDocaryContext
    {
        public DocaryContextStub()
        {
            Seed();
        }

        public IDbSet<Activity> Activities { get; set; }
        public IDbSet<Entry> Entries { get; set; }
        public IDbSet<Location> Locations { get; set; }

        public void Seed()
        {
            Entries = new FakeDbSet<Entry>()
            {
                new Entry() { Id = 1 }
            };            
        }
    }
}
