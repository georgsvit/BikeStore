using BikeStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.Cart
{
    public class Item
    {
        public Model Model { get; set; }
        public ModelColour ModelColour { get; set; }
        public FrameSize FrameSize { get; set; }
        public int Quantity { get; set; }
    }
}
