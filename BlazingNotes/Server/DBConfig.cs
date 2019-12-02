using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace BlazingNotes.Server
{
    public static class DBConfig
    {
        public static void Set(SQLiteConnection con, string key, object value)
        {
            using var cmd = new SQLiteCommand(con)
            {
                CommandText = "INSERT INTO config(key, value) VALUES(@key, @value)"
            };

            _ = cmd.Parameters.AddWithValue("@key", key);
            _ = cmd.Parameters.AddWithValue("@value", value);
            cmd.Prepare();

            var rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected <= 0)
            {
                throw new Exception($"set {key} to {value} failed");
            }
        }

        public static object Get(SQLiteConnection con, string key)
        {
            using var cmd = new SQLiteCommand(con)
            {
                CommandText = "SELECT key,value from config WHERE key = @key"
            };

            _ = cmd.Parameters.AddWithValue("@key", key);
            cmd.Prepare();

            var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return reader[1];
            }
            return null;
        }
    }
}
