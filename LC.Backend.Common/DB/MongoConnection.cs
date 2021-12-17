using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public class MongoConnection : IDbConnection
    {
        public string ConnectionString { get; set ; }
        public string DatabaseName { get ; set ; }
    }
}
