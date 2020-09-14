using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Sex
    {
        public int Id { get; set; }
        [Display(Name = "Значення статі")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Value { get; set; }

        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Sex sex = (Sex)obj;
            return (this.Value == sex.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value);
        }
    }
}
