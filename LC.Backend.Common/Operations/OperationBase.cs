using LC.Backend.Common.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        public Result<TResult> Execute(TInput input, Guid? correlationId, [CallerMemberName] string methodName = null)
        {
            Result<TResult> result = default(Result<TResult>);
            try
            {
                result = CallMethodWraped(() => ExecuteImpl(input), correlationId, methodName);
            }
            catch(Exception x)
            { 
                Logger.LogError(x);
            }
            return result;
        }
        public async Task<Result<TResult>> ExecuteAsync(TInput input, Guid? correlationId, [CallerMemberName] string methodName = null)
        {
            Result<TResult> result = null;
            try
            {
                result =  await CallMethodWrapedAync(async () => await ExecuteImplAsync(input), correlationId, methodName);
            }
            catch (Exception x)
            {
                Logger.LogError(LogObject.Define(correlationId, OperationName, methodName, LogObject.LogStateFailed, x.Message), x, OperationName);
                result = Result<TResult>.Error(x, correlationId: correlationId);
            }
            return result;
        }
        protected async Task<Result<TResult>> CallMethodWrapedAync(Func<Task<Result<TResult>>> method, Guid? correlationId, string methodName)
        {
            var logObject = LogObject.Define(correlationId, OperationName, methodName, LogObject.LogStateStarted);
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
        protected virtual Result<TResult> ExecuteImpl(TInput input) 
        {
            throw new NotImplementedException();
        }
        protected virtual Task<Result<TResult>> ExecuteImplAsync(TInput input) 
        {
            throw new NotImplementedException();
        }
        protected JsonSerializerOptions JsonOptions => Utils.JsonOptions;
        protected ILogger Logger => _logger;
        protected abstract string OperationName { get; }
        protected virtual Result<TResult> CallMethodWraped(Func<Result<TResult>> method, Guid? correlationId, string methodName)
        {
            var logObject = LogObject.Define(correlationId, nameof(TResult), methodName, LogObject.LogStateStarted);
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
