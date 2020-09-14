using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class ModelName
    {
        public int Id { get; set; }
        [Display(Name = "Значення назви")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Value { get; set; }

        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            ModelName name = (ModelName)obj;
            return (this.Value == name.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value);
        }
    }
}
