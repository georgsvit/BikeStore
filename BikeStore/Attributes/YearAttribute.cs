using System;
using System.ComponentModel.DataAnnotations;

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
