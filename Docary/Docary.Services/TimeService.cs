using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}
