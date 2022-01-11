using LCWebAssembly.Services;
using LCWebComposer.Services;
using LCWebLayout.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LCWebComposer
{
    public static class Extentions
    {
        public static void AddWebComposer(this IServiceCollection services)
        {
            services.AddScoped<ILayoutLoader, LayoutLoader>();
            services.AddScoped<IAssembliesLoader, AssembliesLoader>();
            services.AddScoped<ILCComposer, LCComposer>();
        }
    }
}
