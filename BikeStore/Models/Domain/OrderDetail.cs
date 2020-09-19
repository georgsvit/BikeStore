namespace BikeStore.Models.Domain
{
    public class OrderDetail
    {
        public int Id { get; set; }
        //
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        //
        public int BikeId { get; set; }
        public Bike Bike { get; set; }
    }
}
