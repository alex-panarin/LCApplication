﻿using LC.Backend.Common.Operations;
using System;
using System.Text.Json;

namespace LC.Backend.Common.Logging
{
    public class LogObject
    {
        public const string LogStateStarted = nameof(LogStateStarted);
        public const string LogStateCompleted = nameof(LogStateCompleted);
        public const string LogStateFailed = nameof(LogStateFailed);
        public Guid? CorrelationId { get; set; }
        public string LogOperation { get; set; }
        public string LogMethod { get; set; }
        public string LogState { get; set; }
        public string LogResult { get; set; }
        public static LogObject Define(Guid? correlationId, string logOperation, string logMethod, string logState, object logResult = null)
        {
            var logObject = new LogObject
            {
                CorrelationId = correlationId ?? Guid.Empty,
                LogMethod = logMethod,
                LogState = logState,
                LogOperation = logOperation,
                LogResult = SerializeResult(logResult)
            };

            return logObject;
        }

        private static string SerializeResult(object logResult)
        {
            return logResult is String
                ? logResult.ToString()
                : JsonSerializer.Serialize(logResult, Utils.JsonOptions);
        }

        public override string ToString()
        {
            return SerializeResult(this);
        }
    }
}