using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingNotes.Shared;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazingNotes.Server.Controllers
{
    [Route("[controller]")]
    public class PrototypeController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public Document Get()
        {
            var content = System.IO.File.ReadAllText("prototype.md");
            return new Document { Content = content, Title = "PROTOTYPE" };
        }
    }
}
