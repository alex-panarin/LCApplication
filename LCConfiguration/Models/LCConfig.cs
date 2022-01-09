using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCConfiguration.Models
{
    public class LCConfig
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public long Expires { get; set; }

    }
}
