using LC.Backend.Common.DB;
using LC.Services.Logging.Entities;
using System.Threading.Tasks;

namespace LC.Services.Logging.Repositories
{
    public class LogRepositiry : ILogRepository
    {
        private readonly IDbContext<LogRow> _context;

        public LogRepositiry(IDbContext<LogRow> context)
        {
            _context = context;
        }
        public async Task Log(LogRow log)
        {
            await _context.AddAsync(log);
        }
    }
}
