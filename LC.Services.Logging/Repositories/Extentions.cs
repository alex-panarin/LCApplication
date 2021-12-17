using LC.Backend.Common.Events;
using LC.Services.Logging.Entities;

namespace LC.Services.Logging.Repositories
{
    public static class Extentions
    {
        public static LogRow ToLog(this IEvent @event)
        {
            return new LogRow();
        }
    }
}
