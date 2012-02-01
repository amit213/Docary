using System;
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
        [StringLength(350)]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }        

        public DateTime? StoppedOn { get; set; }

        [Required]
        [StringLength(36)]
        public string UserId { get; set; }
    }
}
