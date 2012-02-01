using System.ComponentModel.DataAnnotations;

namespace Docary.Models
{
    public class TimelineColor
    {
        public TimelineColor() { }

        public TimelineColor(string value)
        {
            Value = value;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(15)]
        public string Value { get; set; }
    }
}
