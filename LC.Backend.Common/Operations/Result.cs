using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.Operations
{
    [Serializable]
    public class Result<T>
    {
        public bool IsSuccess { get; protected set; }
        
        public T Data { get; protected set; }
        
        public Guid? CorrelationId { get; protected set; }
        
        public KeyValuePair<string, string>[] Errors { get; protected set; }

        public static Result<T> Success(T data = default(T), Guid? correlationId = null)
            => new Result<T> { Data = data, CorrelationId = correlationId, IsSuccess = true };
        
        public static Result<T> Error(KeyValuePair<string, string> error,  T data = default(T), Guid? correlationId = null)
            => new Result<T> { Errors = new KeyValuePair<string, string>[1] {error}, Data = data, CorrelationId = correlationId, IsSuccess = false};

        public static Result<T> Error(string errorCode, string errorMessage, T data = default(T), Guid? correlationId = null)
            => Error(new KeyValuePair<string, string> (errorCode, errorMessage), data, correlationId);

        public static Result<T> Error(Exception x, T data = default(T), Guid? correlationId = null)
            => Error(x.Message, x.StackTrace, data, correlationId);
    }
}
