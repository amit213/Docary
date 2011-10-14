using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Docary.Models;

namespace Docary.Repositories.EF
{
    public class ActivityRepository : IActivityRepository
    {
        private IDocaryContext _context;

        public ActivityRepository(IDocaryContext context)
        {
            _context = context;
        }

        public IQueryable<Activity> Get()
        {
            return _context.Activities;
        }
        
        public int Add(Activity activity)
        {
            var activityId = _context.Activities.Add(activity).Id;

            _context.SaveChanges();

            return activityId;
        }
    }
}
