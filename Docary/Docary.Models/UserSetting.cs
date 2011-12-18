using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Models
{
    public class UserSetting
    {
        public string UserId { get; set; }
        public TimeZoneInfo TimeZone { get; set; }
    }
}
