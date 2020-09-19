using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class AgeGroup
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "{0} є необхідною")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто короткою або занадто великою")]
        public string Value { get; set; }

        [JsonIgnore]
        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            AgeGroup group = (AgeGroup)obj;
            return (this.Value == group.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value, Model);
        }
    }
}
