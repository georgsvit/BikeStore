﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class AgeGroup
    {
        public int Id { get; set; }
        public string Value { get; set; }
      
        public ICollection<Model> Model { get; set; }
    }
}
