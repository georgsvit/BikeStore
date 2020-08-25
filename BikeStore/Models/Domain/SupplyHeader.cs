using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class SupplyHeader
    {
        public int Id { get; set; }           
        public string Provider { get; set; }
        public DateTime CreationDate { get; set; }
        //
        public int RecipientId { get; set; }
        public User Recipient { get; set; }
        //
        public ICollection<SupplyDetail> SupplyDetail { get; set; }
    }
}
