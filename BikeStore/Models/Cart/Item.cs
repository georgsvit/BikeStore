using BikeStore.Models.Domain;

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
