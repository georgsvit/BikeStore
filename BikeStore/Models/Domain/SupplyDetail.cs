using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class SupplyDetail
    {
        public int Id { get; set; }
        //
        public int BikeId { get; set; }
        public Bike Bike { get; set; }
        //
        public int SupplyHeaderId { get; set; }
        public SupplyHeader SupplyHeader { get; set; }
    }
}
