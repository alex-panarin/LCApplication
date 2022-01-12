using LC.Backend.Common.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;

namespace LC.Backend.Common.Operations
{
    public abstract class OperationBase<TInput, TResult>
    {
        private readonly ILogger _logger;
        public OperationBase(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<TResult>> ExecuteAsync(TInput input, Guid? correlationId, [CallerMemberName] string methodName = null)
        {
            Result<TResult> result = null;
            try
            {
                result = await CallMethodWrapedAync(() => ExecuteImplAsync(input), input, correlationId, methodName);
            }
            catch (Exception x)
            {
                Logger.LogError(LogObject.Define(correlationId, OperationName, methodName, LogObject.LogStateFailed, x.Message), x, OperationName);
                result = Result<TResult>.Error(x, correlationId: correlationId);
            }
            return result;
        }
        protected async Task<Result<TResult>> CallMethodWrapedAync(Func<Task<Result<TResult>>> method, TInput input, Guid? correlationId, string methodName)
        {
            var logObject = LogObject.Define(correlationId, OperationName, methodName, LogObject.LogStateStarted, input);
            var operation = $"{OperationName}.{methodName}";
            Logger.LogInfo(logObject, operation);

            var result = await method();

            if (!result.IsSuccess)
            {
                logObject.LogState = LogObject.LogStateFailed;
                Logger.LogError(logObject, null, operation);
            }
            else
            {
                logObject.LogState = LogObject.LogStateCompleted;
                logObject.LogResult = JsonSerializer.Serialize(result.Data, JsonOptions);
                Logger.LogInfo(logObject, operation);
            }

            return result;
        }
        protected abstract Task<Result<TResult>> ExecuteImplAsync(TInput input);
        protected JsonSerializerOptions JsonOptions => Utils.JsonOptions;
        protected ILogger Logger => _logger;
        protected abstract string OperationName { get; }

    }
}
