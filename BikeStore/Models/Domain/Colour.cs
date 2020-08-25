using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class Colour
    {
        public int Id { get; set; }
        public string ColourValue { get; set; }

        public ICollection<ModelColour> ModelColour { get; set; }
    }
}
