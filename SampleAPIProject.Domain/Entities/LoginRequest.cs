﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLMProject.Domain.Entities
{
    public class LoginRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}
