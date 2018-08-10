using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models
{
    public class AuthenticationModel
    {
        public UserModel UserDetails { get; set; }
        public byte[] VoiceData { get; set; }
        public string ContentLanguage { get; set; } = "en-US";
    }
}