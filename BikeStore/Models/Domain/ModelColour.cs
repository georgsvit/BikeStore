using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BikeStore.Models.Domain
{
    public class ModelColour
    {
        public int Id { get; set; }
        //
        [Required(ErrorMessage = "Будь ласка введіть посилання на зображення")]
        [DataType(DataType.ImageUrl)]
        public string ImageLink { get; set; }
        //
        [JsonIgnore]
        public Model Model { get; set; }
        [Required(ErrorMessage = "Будь ласка оберіть модель")]
        public int ModelId { get; set; }
        //
        public Colour Colour { get; set; }
        [Required(ErrorMessage = "Будь ласка оберіть колір")]
        public int ColourId { get; set; }
        //
        [JsonIgnore]
        public ICollection<Bike> Bike { get; set; }

        public List<int> FrameGetter(Dictionary<string, int> sizes)
        {
            if (Bike != null)
            {
                foreach (Bike b in Bike)
                {
                    if (b.StatusId == 1) sizes[b.FrameSize.Size]++;
                }
            }

            return sizes.Values.ToList();
        }
    }
}
