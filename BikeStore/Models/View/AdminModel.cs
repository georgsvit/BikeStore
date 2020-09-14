using BikeStore.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Models.View
{
    public class AdminModel
    {
        public Model Model { get; set; }
        public int AllBikes { get; set; }
        public int ReservedBikes { get; set; }

        public AdminModel(Model model, int freeBikes, int reservedBikes)
        {
            this.Model = model;
            this.AllBikes = freeBikes;
            this.ReservedBikes = reservedBikes;
        }
    }
}
