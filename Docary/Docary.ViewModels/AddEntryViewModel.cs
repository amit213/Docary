using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

using Docary.Models;

namespace Docary.ViewModels
{
    public class AddEntryViewModel
    {
        [Required]
        public string TagName { get; set; }
        [Required]
        public string LocationName { get; set; }  
        [Required]
        public string Description { get; set; }       
    }
}
