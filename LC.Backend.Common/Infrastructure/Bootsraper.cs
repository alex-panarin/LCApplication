using LC.Backend.Common.DB;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LC.Backend.Common.Infrastructure
{
    public static class Bootsraper
    {
        public static void ConfigureDb(this IServiceProvider service)
        {
            service.GetService<IDbInitializer>().InitializeAsync();
        }
    }
}
