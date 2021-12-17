using LC.Backend.Common.DB;
using System;

namespace LC.Services.Logging.Entities
{
    public class LogRow : IEntity
    {
        public LogRow()
        {
            DateCreated = DateTime.Now;
        }
        public Guid Id { get ; set ; }

        public DateTime DateCreated { get; protected set; }

        public string Operation { get; set; }
        public string Method { get; set; }
        public string Data { get; set; }

    }
}
