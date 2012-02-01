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
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(15)]
        public string Color { get; set; }

        [Required]
        [StringLength(36)]
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
