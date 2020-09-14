using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        [Display(Name = "Назва")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} є занадто короткою або занадто великою")]
        public string Title { get; set; }
        [Display(Name = "Опис")]
        [Required(ErrorMessage = "{0} є необхідним")]
        [StringLength(10000, MinimumLength = 3, ErrorMessage = "{0} є занадто коротким або занадто великим")]
        public string Description { get; set; }

        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            Category group = (Category)obj;
            return (this.Description == group.Description) && (this.Title == group.Title);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Title, Description, Model);
        }
    }
}
