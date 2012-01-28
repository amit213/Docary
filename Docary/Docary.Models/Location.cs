using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Docary.Models
{
    public class Location
    {
        public Location() { }

        public Location(string name, string userId)
        {
            Name = name;
            UserId = userId;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(36)]
        public string UserId { get; set; }
       
    }
}
