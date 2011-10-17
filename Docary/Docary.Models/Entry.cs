using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Docary.Models
{
    public class Entry
    {
        public int Id { get; set; }
        [Required]
        public virtual Activity Activity { get; set; }
        [Required]
        public int ActivityId { get; set; }
        [Required]
        public virtual Location Location { get; set; }
        [Required]
        public int LocationId { get; set; }
        public string Meta { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime StoppedOn { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
