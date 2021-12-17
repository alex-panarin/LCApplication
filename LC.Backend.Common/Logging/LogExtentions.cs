using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace LC.Backend.Common.Logging
{
    public static class LogExtentions
    {
        static ConcurrentDictionary<string, Action<ILogger, LogObject, Exception>> actions = new ConcurrentDictionary<string, Action<ILogger, LogObject, Exception>>();

        public static void LogInfo(this ILogger logger, LogObject logObject, [CallerMemberName] string caller = null)
        {
            var log = actions.GetOrAdd($"{caller}.info", LoggerMessage.Define<LogObject>(LogLevel.Information, new EventId(1, "Info"), "{LogObject}"));
            log(logger, logObject, null);
        }
        public static void LogError(this ILogger logger, LogObject logObject, Exception x, [CallerMemberName] string caller = null)
        {
            var log = actions.GetOrAdd($"{caller}.error", LoggerMessage.Define<LogObject>(LogLevel.Error, new EventId(2, "Error"), "{LogObject}"));
            log(logger, logObject, x);
        }

        public static void LogError(this ILogger logger, Exception x, [CallerMemberName] string caller = null)
        {
           LogError(logger, null, x, caller);
        }
    }
}
