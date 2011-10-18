using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Services;
using Docary.Models;

namespace Docary.ViewModelAssemblers.Test.Stubs
{
    public class EntryServiceStub : IEntryService
    {
        public IEnumerable<Entry> GetEntries()
        {
            return new List<Entry>()
            {
                new Entry() {
                    Activity = new Activity() {
                        Id = 1,
                        Name = "Work"
                    },
                    CreatedOn = DateTime.Now.Date,
                    Id = 1,
                    Location = new Location() {
                        Id = 1,
                        Name = "Brussel"
                    },
                    Meta = "Bla",
                    UserId = "1"
                },
                new Entry() {
                    Activity = new Activity() {
                        Id = 1,
                        Name = "Workout"
                    },
                    CreatedOn = DateTime.Now.Date.AddHours(15),
                    Id = 2,
                    Location = new Location() {
                        Id = 2,
                        Name = "At home"
                    },
                    Meta = "Bla",
                    UserId = "1"
                },
                new Entry() {
                    Activity = new Activity() {
                        Id = 3,
                        Name = "Blog"
                    },
                    CreatedOn = DateTime.Now.AddDays(1),
                    Id = 1,
                    Location = new Location() {
                        Id = 2,
                        Name = "At home"
                    },
                    Meta = "Bla",
                    UserId = "1"
                }
            };
        }

        public IEnumerable<Entry> GetEntries(string user)
        {
            throw new NotImplementedException();
        }

        public void AddEntry(Entry entry)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntry(int id)
        {
            throw new NotImplementedException();
        }
    }
}
