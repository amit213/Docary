using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data.Entity;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class DocaryDbInitializer : CreateDatabaseIfNotExists<DocaryContext> 
    {      
        protected override void Seed(DocaryContext context)
        {
            SeedTimelineColors(context);       
            
            base.Seed(context);
        }     

        private void SeedTimelineColors(DocaryContext context)
        {
            context.TimelineColors.Add(new TimelineColor("#C1DE33"));
            context.TimelineColors.Add(new TimelineColor("#4E5E24"));
            context.TimelineColors.Add(new TimelineColor("#68A0B0"));
            context.TimelineColors.Add(new TimelineColor("#FF0000"));
            context.TimelineColors.Add(new TimelineColor("#68A0B0"));            
        }
    }
}
