using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class Status
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        public ICollection<Bike> Bike { get; set; }
    }
}
