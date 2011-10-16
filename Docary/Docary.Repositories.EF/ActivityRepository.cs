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
        
        public Activity Add(Activity activity)
        {
            var addedActivity = _context.Activities.Add(activity);

            _context.SaveChanges();

            return addedActivity;
        }

        public void Delete(int id)
        {
            var activityToDelete = _context.Activities.Where(a => a.Id == id).First();

            _context.Activities.Remove(activityToDelete);
            _context.SaveChanges();
        }
    }
}
