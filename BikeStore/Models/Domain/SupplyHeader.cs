using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class SupplyHeader
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть будь ласка назву фірми-постачальника")]
        [Display(Name = "Назва фірми-постачальника")]
        [StringLength(25, MinimumLength = 5, ErrorMessage = "{0} є занадто короткою або занадто великою")]
        public string Provider { get; set; }

        public DateTime CreationDate { get; set; }
        //
        public string RecipientId { get; set; }
        [Display(Name = "Співробітник, що прийняв товар")]
        public User Recipient { get; set; }
        //
        public ICollection<SupplyDetail> SupplyDetail { get; set; }
    }
}
