using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class AgeGroup
    {
        public int Id { get; set; }
        public string Value { get; set; }
      
        public ICollection<Model> Model { get; set; }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType()) return false;

            AgeGroup group = (AgeGroup)obj;
            return (this.Value == group.Value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Value, Model);
        }
    }
}
