using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Attributes
{
    public class YearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            int year = (int)value;
            return year >= 2010 && year <= DateTime.Now.Year + 1;
        }
    }
}
