using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Models
{
    public class UserSetting
    {
        public string UserId { get; set; }
        public string TimeZoneId { get; set; }

        public TimeZoneInfo TimeZone
        {
            get
            {
                return TimeZoneInfo.FindSystemTimeZoneById(TimeZoneId);
            }
        }

    }
}
