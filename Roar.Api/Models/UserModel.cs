﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }

        public string Badge { get; set; }
        public string Pin { get; set; }
    }
}