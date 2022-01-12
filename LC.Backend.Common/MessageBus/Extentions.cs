using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Extensions.Client;
using RawRabbit.vNext;

namespace LC.Backend.Common.MessageBus
{
    public static class Extentions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            //var options = configuration.GetSection("rabbitmq").Get<RabbitMqOptions>();
            services.AddRawRabbit(configuration.GetSection("rabbitmq"));
            services.AddSingleton<IBusClient>(new ExtendableBusClient(ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(services)));
        }
    }
}
