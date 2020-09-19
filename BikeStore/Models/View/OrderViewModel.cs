using BikeStore.Models.Domain;
using System.Collections.Generic;

namespace BikeStore.Models.View
{
    public class OrderViewModel
    {
        public OrderHeader orderHeader { get; set; }
        public List<OrderDetail> orderDetails { get; set; } = new List<OrderDetail>() { };
    }
}
