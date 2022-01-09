using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;

namespace LCRegistration
{
    public class LCServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
    {
        private readonly ServiceProviderOptions _options;
        private LCServiceProvider _provider;

        public LCServiceProviderFactory()
        {
            _options = new ServiceProviderOptions();
        }

        public IServiceCollection CreateBuilder(IServiceCollection services)
        {
            return services;
        }

        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
        {
            if(_provider == null)
                _provider = new LCServiceProvider(containerBuilder.BuildServiceProvider(_options));
            return _provider;
        }
    }

    class LCServiceProvider : IServiceProvider, IServiceScopeFactory
    {
        private static readonly ConcurrentDictionary<Type, object> _services = new ConcurrentDictionary<Type, object>();
        private readonly IServiceProvider _provider;

        public LCServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
            Console.WriteLine($"Service provider: {provider}");
        }
        public object GetService(Type serviceType)
        {
            Console.WriteLine($"Get Service: {serviceType}");
            if (serviceType == typeof(IServiceScopeFactory))
                return this;
            var service = _provider.GetService(serviceType);
            return service ?? Services.GetOrAdd(serviceType, service);
        }
        public IServiceScope CreateScope()
        {
            return new LCServiceScope(this);
        }
        public ConcurrentDictionary<Type, object> Services => _services;
    }

    class LCServiceScope : IServiceScope
    {
        public LCServiceScope(IServiceProvider provider)
        {
            Console.WriteLine($"Get Scope: {this}");
            ServiceProvider = provider;
        }
        public IServiceProvider ServiceProvider { get; private set;}

        public void Dispose()
        {
            ServiceProvider = null;
        }
    }
}
