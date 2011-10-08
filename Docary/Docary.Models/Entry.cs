using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Docary.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public virtual Activity Activity { get; set; }
        public int ActivityId { get; set; }
        public virtual Location Location { get; set; }
        public int LocationId { get; set; }
        public string Meta { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }
    }
}
