using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using Docary.Models;
using System.Data.Entity.Validation;

namespace Docary.Repositories.EF
{
    public class EntryRepository : RepositoryBase<Entry>, IEntryRepository
    {
        public EntryRepository(DocaryContext context) : base(context) { }

        public override IQueryable<Entry> Get()
        {    
            return Context.Entries.Include(e => e.Tag).Include(e => e.Location);         
        }

        public IEnumerable<Entry> Get(DateTime createdOnMin, DateTime createdOnMax, string userId)
        {
            var results =
                Get().OrderByDescending(e => e.CreatedOn)
                     .Where(e => e.UserId == userId &&
                          ((e.CreatedOn >= createdOnMin && e.CreatedOn <= createdOnMax) ||
                           (e.CreatedOn <= createdOnMin && e.StoppedOn >= createdOnMin && e.CreatedOn <= createdOnMax)))
                     .OrderBy(e => e.CreatedOn)
                     .ToList();

            var firstResult = results.FirstOrDefault();

            if (firstResult != null)
            {
                firstResult.CreatedOn = createdOnMin.Date;

                results.RemoveAt(0);
                results.Add(firstResult);
                results = results.OrderBy(e => e.CreatedOn).ToList();
            }

            return results;
        }

        public Entry GetLatestEntry(string userId)
        {
            return Get().Where(e => e.StoppedOn == null && e.UserId == userId).FirstOrDefault();
        }       

        public void Update(Entry item)
        {
            var entryToUpdate = Context.Entries.Where(e => e.Id == item.Id).First();

            entryToUpdate.CreatedOn = item.CreatedOn;
            entryToUpdate.Description = item.Description;
            entryToUpdate.LocationId = item.LocationId;
            entryToUpdate.StoppedOn = item.StoppedOn;
            entryToUpdate.TagId = item.TagId;
            entryToUpdate.UserId = item.UserId;            
        }

        public bool IsEmpty(string userId)
        {
            return !Context.Entries.Any(e => e.UserId == userId);
        }

        public int Count(string userId)
        {
            return Get().Count(e => e.UserId == userId);
        }

        public Entry GetFirstRealEntry(string userId)
        {
            return Context.Entries
                    .Where(e => e.UserId == userId)
                    .OrderBy(e => e.CreatedOn)
                    .Skip(1)
                    .Take(1)
                    .FirstOrDefault();
        }
    }
}
