using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class ModelPrefix
    {
        public int Id { get; set; }
        [Display(Name = "Значення префіксу назви")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Value { get; set; }

        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            ModelPrefix prefix = (ModelPrefix)obj;
            return (this.Value == prefix.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value);
        }
    }
}
