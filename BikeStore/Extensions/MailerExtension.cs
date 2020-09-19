using BikeStore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BikeStore.Extensions
{
    public static class MailerExtension
    {
        public static IServiceCollection AddMailer(this IServiceCollection service) => service.AddSingleton<IMailer, Mailer>();
    }
}
