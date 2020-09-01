using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Status
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
        public string Value { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
        public string Description { get; set; }

        public ICollection<Bike> Bike { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Status status = (Status)obj;
            return (this.Value == status.Value && this.Description == status.Description);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value, Description);
        }
    }
}
