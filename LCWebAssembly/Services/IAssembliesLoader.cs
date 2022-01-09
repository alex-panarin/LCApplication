﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace LCWebAssembly.Services
{
    public interface IAssembliesLoader
    {
        Task<IEnumerable<Assembly>> LoadAssembliesAsync(string[] assemblyNames, ILogger logger, WebAssemblyHostBuilder builder);
    }
}
