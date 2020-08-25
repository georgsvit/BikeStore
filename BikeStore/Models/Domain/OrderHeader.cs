using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BikeStore.Models.Domain
{
    public class OrderHeader
    {
        public int Id { get; set; }        
        [Required(ErrorMessage = "Будь ласка введіть адресу доставки")]
        public string ShippingAddress { get; set; }
        public DateTime CreationDate { get; set; }
        //
        public int UserId { get; set; }
        public User User { get; set; }
        //
        public ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
