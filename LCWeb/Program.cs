using LCConfiguration.Models;
using LCConfiguration.Services;
using LCWeb.Repositories;
using LCWebAssembly.Services;
using LCWebLayout.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LCWeb
{
    public class Program
    {
        internal static WebAssemblyHostBuilder DefaultBuilder;
        internal static LCConfig Config;
        public static async Task Main(string[] args)
        {
            DefaultBuilder = WebAssemblyHostBuilder.CreateDefault(args);

            DefaultBuilder.RootComponents.Add<App>("#app");
            DefaultBuilder.Services.AddScoped<IAssembliesLoader, AssembliesLoader>();
            DefaultBuilder.Services.AddScoped<ILayoutLoader, LayoutLoader>();
            DefaultBuilder.Services.AddScoped<ILCConfiguration, Configuration>();
            DefaultBuilder.Services.AddScoped<IUserRepository, UserRepository>();
            DefaultBuilder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(DefaultBuilder.HostEnvironment.BaseAddress) });
            await DefaultBuilder.Build().RunAsync();
        }
    }
}
