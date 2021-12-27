using LC.Backend.Common.MessageBus.RawRabbit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext;
using RawRabbit.Extensions.Client;
using RawRabbit.Context;

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
