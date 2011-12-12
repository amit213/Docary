using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Docary.Models;

namespace Docary.Repositories.EF
{
    public class TimelineColorRepository : RepositoryBase<TimelineColor>, ITimelineColorRepository
    {
        public TimelineColorRepository(DocaryContext context) : base(context) { } 
    }
}
