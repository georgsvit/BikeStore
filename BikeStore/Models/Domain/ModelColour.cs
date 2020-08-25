using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class ModelColour
    {
        public int Id { get; set; }
        public string ImageLink { get; set; }        
        //
        public int ModelId { get; set; }
        public Model Model { get; set; }
        //
        public int ColourId { get; set; }
        public Colour Colour { get; set; }
        //
        public ICollection<Bike> Bike { get; set; }

        public List<int> FrameGetter()
        {
            List<int> output = new List<int>() { 0, 0, 0, 0, 0 };

            foreach (Bike b in Bike)
            {
                if (b.StatusId == 1) output[b.FrameSizeId - 1]++;
            }

            return output;
        }
    }
}
