using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
