using LC.Backend.Common.MessageBus.RawRabbit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.vNext;
using IBusClient = RawRabbit.IBusClient;

namespace LC.Backend.Common.MessageBus
{
    public static class Extentions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("rabbitmq").Get<RabbitMqOptions>();
            services.AddSingleton<IBusClient>(_ => BusClientFactory.CreateDefault(options));
        }
        
    }
}
