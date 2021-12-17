using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.DB
{
    public interface IDbTables
    {
        string[] Tables { get; set; }
    }

    public class DbTables : IDbTables
    {
        public string[] Tables { get ; set ; }
    }
}
