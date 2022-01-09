using System;

namespace LCRegistration
{
    public static class Extentions
    {
        public static void AddService(this IServiceProvider provider, Type serviceType, Type implementationType)
        {
            provider.AddService(serviceType, Activator.CreateInstance(implementationType));
        }
        public static void AddService(this IServiceProvider provider, Type serviceType, object service)
        {
            Console.WriteLine($"Register service: {service}");
            (provider as LCServiceProvider)?
                .Services
                .AddOrUpdate(serviceType, service, (k, v) => v);
        }
    }
}
