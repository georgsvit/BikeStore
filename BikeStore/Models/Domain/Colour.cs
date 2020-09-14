using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Colour
    {
        public int Id { get; set; }
        [Display(Name = "Колір")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Значення {0}у є занадто коротким або занадто великим")]
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
