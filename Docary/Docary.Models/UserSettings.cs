using System;
using System.ComponentModel.DataAnnotations;

namespace Docary.Models
{
    public class UserSettings
    {
        public int Id { get; set; }

        [Required]
        [StringLength(36)]
        public string UserId { get; set; }

        [Required]        
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
