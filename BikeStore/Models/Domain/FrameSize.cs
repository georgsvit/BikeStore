using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class FrameSize
    {
        public int Id { get; set; }
        [Display(Name = "Розмір")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(3, MinimumLength = 1, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Size { get; set; }
        [Display(Name = "Мінімальний зріст")]
        [Required(ErrorMessage = "{0} є необхідним")]
        public int MinHeight { get; set; }
        [Display(Name = "Максимальний зріст")]
        [Required(ErrorMessage = "{0} є необхідним")]
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
