using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
        public string Title { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(10000, MinimumLength = 3, ErrorMessage = "{0} is too short or too long")]
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
