using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using BlazingNotes.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazingNotes.Server.Controllers
{
    [Route("[controller]")]
    public class NotesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public List<DocumentMeta> Get()
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };
            var output = new List<DocumentMeta>();
            foreach(var file in Directory.EnumerateFiles("notes")) {
                
                var json = System.IO.File.ReadAllText(file);
                System.Diagnostics.Debug.WriteLine(json);
                var doc = JsonSerializer.Deserialize<Document>(json, options);
                var meta = new DocumentMeta {
                    Title = doc.Title,
                    Created = System.IO.File.GetCreationTime(file),
                    Id = Path.GetFileNameWithoutExtension(file)
                };
                output.Add(meta);
            }
            return output;
        }

        [HttpGet("{id}")]
        public Document GetDocument(string id)
        {
            var options = new JsonSerializerOptions
            {
                AllowTrailingCommas = true
            };
            var path = $"./notes/{id}.json";
            var json = System.IO.File.ReadAllText(path);
            return JsonSerializer.Deserialize<Document>(json, options);
        }
    }
}
