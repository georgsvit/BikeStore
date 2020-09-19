using BikeStore.Filter;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BikeStore.Attributes
{
    public class ForAdminAttribute : Attribute, IFilterFactory
    {
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider) =>
            new ForAdminFilter();

        public bool IsReusable => false;
    }
}
