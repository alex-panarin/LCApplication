using LCConfiguration.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace LCConfiguration.Services
{
    public interface ILCConfiguration
    {
        Task<LCConfig> GetConfigurationAsync(HttpClient client, string userName, string userRole, string password);
    }
}
