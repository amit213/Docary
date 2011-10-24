﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Docary.Models
{
    public class EntryTag
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Color { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}