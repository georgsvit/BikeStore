using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BikeStore.Models.Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Mailing { get; set; }

        public virtual ICollection<OrderHeader> OrderHeader { get; set; }
        public virtual ICollection<SupplyHeader> SupplyHeader { get; set; }

        public string GetFullName
        {
            get => FirstName + " " + LastName;
        }
    }
}
