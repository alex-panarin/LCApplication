﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.Auth
{
    public class JsonWebToken 
    {
        public string Token { get; set; }
        public long Expires { get; set; }
    }
}
