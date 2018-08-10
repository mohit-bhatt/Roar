﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models
{
    public class EnrollmentModel
    {
        public UserModel UserDetails { get; set; }
        public byte[] VoiceData { get; set; }
        public string ContentLanguage { get; set; } = "en-US";
        public string EnrollmentId { get; set; }
    }
}