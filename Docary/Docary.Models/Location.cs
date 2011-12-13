using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

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
        public string Name { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
