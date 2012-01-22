using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Docary.Models
{
    public class TimelineColor
    {       

        public TimelineColor(string value)
        {
            Value = value;
        }

        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
