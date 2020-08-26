using BikeStore.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeStore.Extensions
{
    public static class MailerExtension
    {
        public static IServiceCollection AddMailer(this IServiceCollection service) => service.AddSingleton<IMailer, Mailer>();
    }
}
