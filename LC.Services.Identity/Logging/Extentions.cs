using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LC.Services.Identity.Logging
{
    public static class Extentions
    {
        public static IServiceCollection AddMqLogging(this IServiceCollection service)
        {
            service.AddLogging(config =>
            {
                config.ClearProviders();
            });
            return service.AddSingleton<ILogger, RabbitMqLogger>();
        }
    }
}
