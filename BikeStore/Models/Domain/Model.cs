using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Model
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть ціну")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть опис")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть модельний рік")]
        public int Year { get; set; }
        public double WheelSize { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть статєву групу")]
        public int SexId { get; set; }
        public Sex Sex { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть вікову групу")]
        public int AgeGroupId { get; set; }
        public AgeGroup AgeGroup { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть назву")]
        public int ModelNameId { get; set; }
        public ModelName ModelName { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть префікс назви")]
        public int ModelPrefixId { get; set; }
        public ModelPrefix ModelPrefix { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть тип підвіски")]
        public int SuspensionId { get; set; }
        public Suspension Suspension { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть категорію")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        //
        public ICollection<ModelColour> ModelColour { get; set; }

        public string FullNameWithYear
        {
            get => (ModelName != null && ModelPrefix != null) ? "GT " + ModelName.Value + " " + ModelPrefix.Value + " " + Year : "GT";
        }

        public string FullName
        {
            get => (ModelName != null && ModelPrefix != null) ? "GT " + ModelName.Value + " " + ModelPrefix.Value : "GT";
        }

        public string Name
        {
            get => (ModelName != null) ? ModelName.Value : "Name";
        }

        public string Prefix
        {
            get => (ModelPrefix != null) ? ModelPrefix.Value : "Prefix";
        }
    }
}
