using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class Bike
    {
        public int Id { get; set; }
        public string FrameNumber { get; set; }
        
        public int StatusId { get; set; }
        public Status Status { get; set; }
        //
        public int StoringPlaceId { get; set; }
        public StoringPlace StoringPlace { get; set; }
        //
        public int FrameSizeId { get; set; }
        public FrameSize FrameSize { get; set; }
        //
        public int ModelColourId { get; set; }
        public ModelColour ModelColour { get; set; }
        //
        public ICollection<OrderDetail> OrderDetail { get; set; }
        public ICollection<SupplyDetail> SupplyDetail { get; set; }
    }
}
