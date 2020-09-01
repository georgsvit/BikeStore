using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class FrameSize
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "{0} is too short or too long")]
        public string Size { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int MinHeight { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int MaxHeight { get; set; }

        public ICollection<Bike> Bike { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            FrameSize size = (FrameSize)obj;
            return (
                this.Size == size.Size && 
                this.MinHeight == size.MinHeight && 
                this.MaxHeight == size.MaxHeight);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Size, MinHeight, MaxHeight);
        }
    }
}
