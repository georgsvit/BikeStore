using BikeStore.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BikeStore.Models.Domain
{
    public class Model
    {
        public int Id { get; set; }
        //
        [DataType(DataType.Currency)]
        [Display(Name = "Вартість")]
        [Required(ErrorMessage = "Будь ласка введіть ціну")]
        [Range(1, 1000000, ErrorMessage = "Будь ласка введіть ціну з діапзону (1 ... 1 000 000)")]
        public double Price { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть опис")]
        [Display(Name = "Опис")]
        [StringLength(10000, MinimumLength = 10, ErrorMessage = "{0} занадто короткий або занадто великий")]
        public string Description { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть модельний рік")]
        [Year(ErrorMessage = "Будь ласка введіть модільний рік з діапазону (2010 ... поточний + 1)")]
        [Display(Name = "Модельний рік")]
        public int Year { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть модельний рік")]
        [WheelSize(new double[] { 20, 24, 26, 27.5, 29 }, ErrorMessage = "Недійсне значення розміру коліс. Введіть одне із значень (20, 24, 26, 27.5, 29)")]
        [Display(Name = "Розмір коліс (у дюймах)")]
        public double WheelSize { get; set; }
        //
        [Display(Name = "Стать")]
        public Sex Sex { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть статєву групу")]
        public int SexId { get; set; }
        //
        [Display(Name = "Вікова група")]
        public AgeGroup AgeGroup { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть вікову групу")]
        public int AgeGroupId { get; set; }
        //
        [Display(Name = "Назва")]
        public ModelName ModelName { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть назву")]
        public int ModelNameId { get; set; }
        //
        [Display(Name = "Префікс назви")]
        public ModelPrefix ModelPrefix { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть префікс назви")]
        public int ModelPrefixId { get; set; }
        //
        [Display(Name = "Тип підвіски")]
        public Suspension Suspension { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть тип підвіски")]
        public int SuspensionId { get; set; }
        //
        [Display(Name = "Категорія")]
        public Category Category { get; set; }
        [Required(ErrorMessage = "Будь ласка введіть категорію")]
        public int CategoryId { get; set; }
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

        public List<int> ModelColoursId
        {
            get => ModelColour == null ? new List<int>() { } : ModelColour.Select(mc => mc.Id).ToList();
        }
    }
}
