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

        public IQueryable<Activity> GetActivities()
        {
            return _context.Activities;
        }
        
        public int AddActivity(Activity activity)
        {
            var activityId = _context.Activities.Add(activity).Id;

            _context.SaveChanges();

            return activityId;
        }
    }
}
