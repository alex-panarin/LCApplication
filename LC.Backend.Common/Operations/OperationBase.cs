using LC.Backend.Common.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LC.Backend.Common.Operations
{
    public abstract class OperationBase<T>
    {
        private readonly ILogger _logger;
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            IgnoreReadOnlyFields = true,
            IgnoreReadOnlyProperties = true,
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        public OperationBase(ILogger logger, Guid? correlationId)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            CorrelationId = correlationId;
        }
        public OperationResult<T> Execute()
        {
            OperationResult<T> result = default(OperationResult<T>);
            try
            {
                result = CallMethodWraped(() => ExecuteImpl());
            }
            catch(Exception x)
            { 
                Logger.LogError(x);
            }
            return result;
        }
        protected abstract OperationResult<T> ExecuteImpl();
        protected JsonSerializerOptions JsonOptions => _options;
        protected ILogger Logger => _logger;
        protected Guid? CorrelationId { get; }
        protected virtual OperationResult<T> CallMethodWraped(Func<OperationResult<T>> method, [CallerMemberName] string methodName = null)
        {
            var logObject = LogObject.Define(CorrelationId, nameof(T), methodName, LogObject.LogStateStarted);
            Logger.LogInfo(logObject, methodName);

            var result = method();

            if (!result.IsSuccess)
            {
                logObject.LogState = LogObject.LogStateFailed;
                Logger.LogError(logObject, null, methodName);
            }
            else
            {
                logObject.LogState = LogObject.LogStateCompleted;
                logObject.LogResult = JsonSerializer.Serialize(result.Data, JsonOptions);
                Logger.LogInfo(logObject, methodName);
            }

            return result;
        }
    }
}
