using System;
using System.Collections.Generic;
using System.Linq;

namespace LCRegistration
{
    public static class Extentions
    {
        public static void AddService(this IServiceProvider provider, Type serviceType, Type implementationType)
        {
            (provider as LCServiceProvider)?
                .Services
                .AddOrUpdate(serviceType, implementationType, (k, v) => v);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }
    }
}
