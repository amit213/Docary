using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data.Entity;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class DocaryDbInitializer : DropCreateDatabaseIfModelChanges<DocaryContext> 
    {
        protected override void Seed(DocaryContext context)
        {
            var homeLocation = context.Locations.Add(new Location() { Name = "Home", UserId = "1" });
            var workTag = context.Tags.Add(new EntryTag() { Name = "Work", UserId = "1" });

            context.Entries.Add(new Entry()
            {
                Tag = workTag,
                Location = homeLocation,
                CreatedOn = DateTime.Now,
                StoppedOn = null,
                Description = "Blablabla",
                UserId = "1"
            });

            context.Entries.Add(new Entry()
            {
                Tag = workTag,
                Location = homeLocation,
                CreatedOn = DateTime.Now,
                StoppedOn = null,
                Description = "More blablablabla",
                UserId = "1"
            });
         
            base.Seed(context);
        }    
    }
}
