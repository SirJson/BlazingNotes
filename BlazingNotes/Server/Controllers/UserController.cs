using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingNotes.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SQLite;
using System.Text;

namespace BlazingNotes.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public string Get() => "Verlaufen?";

        [HttpPost]
        public void Register(RegisterRequest req)
        {
            using var con = new SQLiteConnection("URI=file:data.db");
            con.Open();

            var salt = (byte[])DBConfig.Get(con, "salt");
            var saltedSecret = req.Secret + Encoding.UTF8.GetString(salt);
            var hashedSecret = Sodium.PasswordHash.ArgonHashString(saltedSecret);

            using var cmd = new SQLiteCommand(con)
            {
                CommandText = "INSERT INTO users(username, secret) VALUES(@user, @secret)"
            };
            
            _ = cmd.Parameters.AddWithValue("@user", req.Username);
            _ = cmd.Parameters.AddWithValue("@secret", hashedSecret);
            cmd.Prepare();

            var rowsAffected = cmd.ExecuteNonQuery();
            if(rowsAffected <= 0)
            {
                throw new Exception("user insert failed");
            }
        }
    }
}