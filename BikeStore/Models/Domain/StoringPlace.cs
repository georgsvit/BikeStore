using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class StoringPlace
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
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
