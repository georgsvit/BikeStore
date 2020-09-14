using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class Bike
    {
        public int Id { get; set; }
        [Display(Name = "Номер рами")]
        [Required(ErrorMessage = "{0} є необхідною")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} має містити 10 символів")]
        public string FrameNumber { get; set; }
        //
        [Display(Name = "Статус")]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        //
        [Display(Name = "Місце збереження")]
        public int StoringPlaceId { get; set; }
        public StoringPlace StoringPlace { get; set; }
        //
        [Display(Name = "Розмір рами")]
        public int FrameSizeId { get; set; }
        public FrameSize FrameSize { get; set; }
        //
        [Display(Name = "Модель та кольорова схема")]
        public int ModelColourId { get; set; }
        public ModelColour ModelColour { get; set; }
        //
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<SupplyDetail> SupplyDetail { get; set; }
    }
}
