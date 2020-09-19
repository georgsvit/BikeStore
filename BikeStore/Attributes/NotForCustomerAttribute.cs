using BikeStore.Filter;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BikeStore.Attributes
{
    public class NotForCustomerAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) => new NotForCustomerFilter();
    }
}
