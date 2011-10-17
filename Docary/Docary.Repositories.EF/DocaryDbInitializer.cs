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
            var workActivitiy = context.Activities.Add(new Activity() { Name = "Work", UserId = "1" });

            context.Entries.Add(new Entry()
            {
                Activity = workActivitiy,
                Location = homeLocation,
                CreatedOn = DateTime.Now,
                StoppedOn = DateTime.MaxValue,
                Meta = "Blablabla",
                UserId = "1"
            });

            context.Entries.Add(new Entry()
            {
                Activity = workActivitiy,
                Location = homeLocation,
                CreatedOn = DateTime.Now,
                StoppedOn = DateTime.MaxValue,
                Meta = "More blablablabla",
                UserId = "1"
            });
         
            base.Seed(context);
        }    
    }
}
