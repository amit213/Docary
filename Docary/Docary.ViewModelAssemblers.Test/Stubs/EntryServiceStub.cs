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
                    CreatedOn = DateTime.Now,
                    Id = 1,
                    Location = new Location() {
                        Id = 1,
                        Name = "Brussel"
                    },
                    Meta = "Bla",
                    UserId = 1
                },
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
