﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.Backend.Common.Commands.Models
{
    public class Authenticate 
    {
        public string Email { get; set; }
        public string Password{ get; set; }
    }
}
