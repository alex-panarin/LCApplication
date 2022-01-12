using LCWebAssembly.Services;
using LCWebLayout.Models;
using LCWebLayout.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LCWebComposer.Services
{
    public class LCComposer : ILCComposer
    {
        private readonly ILayoutLoader _layoutLoader;
        private readonly IAssembliesLoader _assembliesLoader;
        private static readonly ConcurrentDictionary<string, LCLayout> _layouts = new ConcurrentDictionary<string, LCLayout>();
        public LCComposer(ILayoutLoader layoutLoader,
            IAssembliesLoader assembliesLoader)
        {
            _layoutLoader = layoutLoader ?? throw new ArgumentNullException(nameof(layoutLoader));
            _assembliesLoader = assembliesLoader ?? throw new ArgumentNullException(nameof(assembliesLoader));
        }

        public async Task Compose(string contextPath, IServiceProvider serviceProvider, ILogger logger)
        {
            logger.LogInformation($"Loading {contextPath}");
            LoadedAssemblies.Clear();

            var layout = _layouts.GetOrAdd(contextPath, await _layoutLoader.LoadLayoutAsync(contextPath, logger));
            if (layout == null)
                return;

            var assemblies = await _assembliesLoader.LoadAssembliesAsync(layout.Assemblies, logger, serviceProvider);
            LoadedAssemblies.AddRange(assemblies);
        }

        public List<Assembly> LoadedAssemblies { get; } = new List<Assembly> { };
    }
}
