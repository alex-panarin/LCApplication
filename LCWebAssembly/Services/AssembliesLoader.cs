﻿using LCRegistration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace LCWebAssembly.Services
{
    public class AssembliesLoader : IAssembliesLoader
    {
        private readonly HttpClient _client;

        public AssembliesLoader(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Assembly>> LoadAssembliesAsync(string[] assemblyNames, ILogger logger, IServiceProvider provider)
        {
            var assemblyList = new List<Assembly>();
            foreach (var assemblyName in assemblyNames)
            {
                var path = Path.Combine("_framework", assemblyName);

                Stream streamdll = null, streamPdb = null;
                try
                {
                    streamdll = await _client.GetStreamAsync(path + ".dll");
                    streamPdb = await _client.GetStreamAsync(path + ".pdb");
                }
                catch(HttpRequestException x)
                {
                    logger?.LogError(x, nameof(LoadAssembliesAsync));
                }
                if (streamdll != null)
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromStream(streamdll, streamPdb);
                    var registers = assembly
                        .GetTypes()
                        .Where(t => t.GetCustomAttribute<RegistrationAttribute>() != null)
                        .ToArray();

                    foreach (var register in registers)
                    {
                        var regType = register.GetCustomAttribute<RegistrationAttribute>().RegistrationType;
                        provider.AddService(regType, register);
                    }

                    assemblyList.Add(assembly);
                    await streamdll.DisposeAsync();
                    if(streamPdb != null)
                        await streamPdb.DisposeAsync();
                }
            }
            return assemblyList;
        }
    }
}
