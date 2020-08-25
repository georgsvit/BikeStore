using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class StoringPlace
    {
        public int Id { get; set; }
        public string Place { get; set; }

        public ICollection<Bike> Bike { get; set; }
    }
}
