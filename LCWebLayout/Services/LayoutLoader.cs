using LCWebLayout.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LCWebLayout.Services
{
    public class LayoutLoader : ILayoutLoader
    {
        private readonly HttpClient _client;
        public LayoutLoader(HttpClient client)
        {
            _client = client;
        }
        public async Task<LCLayout> LoadLayoutAsync(string configKey, ILogger logger = null)
        {
            var array = await _client.GetFromJsonAsync<LayoutArray>("layout.json", new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            logger?.LogInformation($"==> Layouts: {array} <==");

            return array
                .Layouts
                .FirstOrDefault(l => string.Equals(l.Key, configKey, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
