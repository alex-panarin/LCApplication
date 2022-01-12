using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LC.Backend.Common.DB
{
    public static class Extentions
    {
        public static void AddMongoDb(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<MongoConnection>(configuration.GetSection("mongo"));
            service.Configure<DbTables>(configuration.GetSection("tables"));
            service.AddSingleton<IDbTables, DbTables>(p => p.GetService<IOptions<DbTables>>().Value);
            service.AddSingleton(config =>
            {
                var options = config.GetService<IOptions<MongoConnection>>();
                return new MongoClient(options.Value.ConnectionString);
            });
            service.AddSingleton<IMongoDatabase>(config =>
            {
                var options = config.GetService<IOptions<MongoConnection>>();
                var client = config.GetService<MongoClient>();
                return client?.GetDatabase(options.Value.DatabaseName);
            });
            service.AddSingleton<IDbInitializer, MongoDb>();
        }
    }
}

