using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace BlazingNotes.Shared
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Secret { get; set; }
    }
}
