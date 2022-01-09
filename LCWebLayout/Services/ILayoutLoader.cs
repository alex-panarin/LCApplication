using LCWebLayout.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LCWebLayout.Services
{
    public interface ILayoutLoader
    {
        Task<LCLayout> LoadLayoutAsync(string configKey, ILogger logger);
    }
}
