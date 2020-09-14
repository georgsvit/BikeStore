using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class StoringPlace
    {
        public int Id { get; set; }
        [Display(Name = "Значення місця")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Place { get; set; }

        public ICollection<Bike> Bike { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            StoringPlace place = (StoringPlace)obj;
            return (this.Place == place.Place);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Place);
        }
    }
}
