using BikeStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.View
{
    public class OrderViewModel
    {
        public OrderHeader orderHeader { get; set; }
        public List<OrderDetail> orderDetails { get; set; } = new List<OrderDetail>() { };
    }
}
