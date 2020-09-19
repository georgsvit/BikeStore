using BikeStore.Models.Domain;
using System.Collections.Generic;

namespace BikeStore.Models.View
{
    public class SupplyViewModel
    {
        public SupplyHeader supplyHeader { get; set; }
        public List<SupplyDetail> supplyDetails { get; set; } = new List<SupplyDetail>() { };
    }
}
