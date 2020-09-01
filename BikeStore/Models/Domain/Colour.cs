using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Colour
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
        public string ColourValue { get; set; }

        public ICollection<ModelColour> ModelColour { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Colour colour = (Colour)obj;
            return (this.ColourValue == colour.ColourValue);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, ColourValue);
        }
    }
}
