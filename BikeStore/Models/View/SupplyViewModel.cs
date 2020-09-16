using BikeStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.View
{
    public class SupplyViewModel
    {
        public SupplyHeader supplyHeader { get; set; }
        public List<SupplyDetail> supplyDetails { get; set; } = new List<SupplyDetail>() { };
    }
}
