using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Suspension
    {
        public int Id { get; set; }
        [Display(Name = "Значення типу")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Type { get; set; }
        [Display(Name = "Опис")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Suspension suspension = (Suspension)obj;
            return (this.Type == suspension.Type && this.Description == suspension.Description);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Type, Description);
        }
    }
}
