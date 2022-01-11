using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LCWebComposer.Services
{
    public interface ILCComposer
    {
        List<Assembly> LoadedAssemblies { get; }
        Task Compose(string contextPath, IServiceProvider serviceProvider, ILogger logger);
    }
}