using LCConfiguration.Services;
using LCRegistration;
using LCWeb.Repositories;
using LCWebComposer;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LCWeb
{
    public class Program
    {
        internal static IServiceProvider DefaultProvider;
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.ConfigureContainer(new LCServiceProviderFactory());
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddWebComposer();
            builder.Services.AddScoped<ILCConfiguration, Configuration>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            WebAssemblyHost host = builder.Build();
            DefaultProvider = host.Services;
            await host.RunAsync();
        }
    }
}
