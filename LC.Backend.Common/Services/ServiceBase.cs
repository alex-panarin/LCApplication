using LC.Backend.Common.Operations;
using System;
using System.Threading.Tasks;

namespace LC.Backend.Common.Services
{
    public class ServiceBase
    {
        protected Result<TResult> InvokeWraped<TResult>(Func<TResult> func, Guid? correlationId = null)
        {
            try
            {
                var result = func();

                return Result<TResult>.Success(result, correlationId);
            }
            catch (Exception x)
            {
                return Result<TResult>.Error(x, correlationId: correlationId);
            }
        }

        protected async Task<Result<TResult>> InvokeWrapedAsync<TResult>(Func<Task<TResult>> func, Guid? correlationId = null)
        {
            try
            {
                var result = await func();

                return Result<TResult>.Success(result, correlationId);
            }
            catch (Exception x)
            {
                return Result<TResult>.Error(x, correlationId: correlationId);
            }
        }

        protected async Task<Result<bool>> InvokeWrapedAsync2(Func<Task> func, Guid? correlationId = null)
        {
            try
            {
                await func();
                return Result<bool>.Success(true, correlationId);

            }
            catch (Exception x)
            {
                return Result<bool>.Error(x, correlationId: correlationId);
            }
        }
    }
}
