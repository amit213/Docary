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

        public void Update(Entry item)
        {
            var entryToUpdate = Context.Entries.Where(e => e.Id == item.Id).First();

            entryToUpdate.CreatedOn = item.CreatedOn;
            entryToUpdate.Description = item.Description;
            entryToUpdate.LocationId = item.LocationId;
            entryToUpdate.StoppedOn = item.StoppedOn;
            entryToUpdate.TagId = item.TagId;
            entryToUpdate.UserId = item.UserId;

            Context.SaveChanges();
        }
    }
}
