using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazingNotes.Shared
{
    public class Document
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
