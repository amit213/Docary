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
        public virtual EntryTag Tag { get; set; }
        [Required]
        public int TagId { get; set; }
        [Required]
        public virtual Location Location { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        [Required]
        public DateTime StoppedOn { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
