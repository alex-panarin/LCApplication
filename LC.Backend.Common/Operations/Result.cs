using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LC.Backend.Common.Operations
{
    public class Result<T>
    {
        public bool IsSuccess { get; protected set; }

        public T Data { get; protected set; }

        public Guid? CorrelationId { get; protected set; }

        [JsonIgnore]
        protected List<string> Errors { get; } = new List<string> { };

        public Result<T> AddError(string error)
        {
            Errors.Add(error);
            return this;
        }

        public string ErrorMessages => $"{string.Join(";", Errors)}";

        public static Result<T> Success(T data = default(T), Guid? correlationId = null)
            => new Result<T> { Data = data, CorrelationId = correlationId, IsSuccess = true };

        public static Result<T> Error(string error, T data = default(T), Guid? correlationId = null)
            => new Result<T> { Data = data, CorrelationId = correlationId, IsSuccess = false }.AddError(error);

        public static Result<T> Error(Exception x, T data = default(T), Guid? correlationId = null)
            => Error(x.Message, data, correlationId);
    }
}
