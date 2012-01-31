using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.MvcExtensions
{
    public class UserSession
    {
        public DateTime? HomeIndexFrom { get; set; }
        public DateTime? HomeIndexTo { get; set; }
       
        public DateTime? HomeStatisticsFrom { get; set; }
        public DateTime? HomeStatisticsTo { get; set; }
    }
}
