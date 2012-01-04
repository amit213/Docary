using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Docary.Models
{
    public class EntryTag
    {
        public EntryTag() { }

        public EntryTag(string name, string color, string userId)
        {
            Name = name;
            Color = color;
            UserId = userId;
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string UserId { get; set; }

        public string TitleCasedName
        {
            get
            {
                if (Name == null)
                    return string.Empty;

                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Name);
            }
        }
    }
}
