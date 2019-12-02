using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazingNotes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        [HttpGet]
        public string DoSetup()
        {
            using var con = new SQLiteConnection("URI=file:data.db");
            con.Open();
            using var initCmd = new SQLiteCommand(con)
            {
                CommandText = System.IO.File.ReadAllText("setup.sql")
            };
            initCmd.Prepare();
            var rows = initCmd.ExecuteNonQuery();
            Console.WriteLine(rows);
            DBConfig.Set(con, "salt", Sodium.PasswordHash.ArgonGenerateSalt());
            return "Setup OK";
        }
    }
}