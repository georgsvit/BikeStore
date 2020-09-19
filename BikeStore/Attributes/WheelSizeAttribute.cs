using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BikeStore.Attributes
{
    public class WheelSizeAttribute : ValidationAttribute
    {
        private double[] _sizes;

        public WheelSizeAttribute(double[] sizes)
        {
            _sizes = sizes;
        }

        public override bool IsValid(object value)
        {
            return value != null && _sizes.Contains((double)value);
        }
    }
}
