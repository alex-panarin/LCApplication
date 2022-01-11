using LCConfiguration.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace LCConfiguration.Services
{
    public class Configuration : ILCConfiguration
    {
        private readonly IConfiguration _config;

        public Configuration(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<LCConfig> GetConfigurationAsync(HttpClient client, string userName, string userRole, string password)
        {
            //var content =  JsonContent.Create(new AuthRequest { Password = password, Email = userName});
            //var response = await client.PostAsJsonAsync(_config.GetSection("ConfigUrl").Value, content);
            //var data = await response
            //    .EnsureSuccessStatusCode()
            //    .Content
            //    .ReadAsStringAsync();
            //var token = JsonSerializer.Deserialize<AuthResponce>(data, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
            var token = new AuthResponce { Token = "123-123132-123132132-12321", Expires = 50000000 };
            return await Task.FromResult( new LCConfig { Key = userRole, Token = token.Token, Expires = token.Expires });
        }

        class AuthRequest
        {
            public string Password { get; set; }
            public string Email { get; set; }
        }

        class AuthResponce
        {
            public string Token { get; set; }
            public long Expires { get; set; }
        }
    }
}
