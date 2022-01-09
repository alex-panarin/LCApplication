using LCWebLayout.Models;
using System.Text.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

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
            var response = await _client.GetAsync("layout.json");
            var jsonString = await response.Content.ReadAsStringAsync();
            logger?.LogInformation($"==> Layouts: {jsonString} <==");

            var array = JsonSerializer.Deserialize<LayoutArray>(jsonString);
            return array
                .Layouts
                .FirstOrDefault(l => l.Key == configKey);
        }
    }
}
