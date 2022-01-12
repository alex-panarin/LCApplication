using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Linq;

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
            if (_provider == null)
                _provider = new LCServiceProvider(containerBuilder.BuildServiceProvider(_options));
            return _provider;
        }
    }

    class LCServiceProvider : IServiceProvider, IServiceScopeFactory
    {
        private readonly ConcurrentDictionary<Type, Type> _services = new ConcurrentDictionary<Type, Type>();
        private readonly ConcurrentDictionary<Type, object> _objects = new ConcurrentDictionary<Type, object>();
        private readonly IServiceProvider _provider;

        public LCServiceProvider(IServiceProvider provider)
        {
            _provider = provider;
        }
        public object GetService(Type serviceType)
        {
            Console.WriteLine($"Get Service: {serviceType}");
            if (serviceType == typeof(IServiceScopeFactory))
                return this;

            var service = _provider.GetService(serviceType);
            if (service != null)
                return service;

            var implType = Services.GetOrAdd(serviceType, service?.GetType());
            if (implType == null)
                return null;

            return _objects.GetOrAdd(serviceType, GetServiceImpl(implType));
        }
        public IServiceScope CreateScope()
        {
            return new LCServiceScope(this);
        }
        public ConcurrentDictionary<Type, Type> Services => _services;

        private object GetServiceImpl(Type implementationType)
        {
            var constructor = implementationType
                .GetConstructors()
                .FirstOrDefault();
            var parameters = constructor?.GetParameters();
            var service = constructor?.Invoke(parameters?.Select(p => GetService(p.ParameterType)).ToArray());
            Console.WriteLine($"Create service: {service}");

            return service;
        }
    }

    class LCServiceScope : IServiceScope
    {
        public LCServiceScope(IServiceProvider provider)
        {
            Console.WriteLine($"Get Scope: {this}");
            ServiceProvider = provider;
        }
        public IServiceProvider ServiceProvider { get; private set; }

        public void Dispose()
        {
            ServiceProvider = null;
        }
    }
}
