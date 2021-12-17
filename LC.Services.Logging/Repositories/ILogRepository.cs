using LC.Services.Logging.Entities;
using System.Threading.Tasks;

namespace LC.Services.Logging.Repositories
{
    public interface ILogRepository
    {
        Task Log(LogRow log);
    }
}
