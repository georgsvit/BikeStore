using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class FrameSize
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }

        public ICollection<Bike> Bike { get; set; }
    }
}
