using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.Operations
{
    [DataContract]
    public class OperationResult<T> : Result<T> 
    {
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public IEnumerable<string> ValidationErrors { get; set; }
    }
}
