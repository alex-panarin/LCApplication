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
            services.AddSingleton<ILayoutLoader, LayoutLoader>();
            services.AddSingleton<IAssembliesLoader, AssembliesLoader>();
            services.AddSingleton<ILCComposer, LCComposer>();
        }
    }
}
