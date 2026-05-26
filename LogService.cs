using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class LogService
    {
        private ConnectionDB _db = new ConnectionDB();

        public void AddLog(string username, string action)
        {
            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    @"INSERT INTO Logs (Username, Actiontext)
                      VALUES (@username, @actiontext)",
                    conn);

                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("actiontext", action);

                cmd.ExecuteNonQuery();
            }
        }

        public void GetLogs()
        {
            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(
                    @"SELECT Username, Actiontext, CreatedAt
                      FROM Logs
                      ORDER BY CreatedAt DESC",
                    conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(
                            $"{reader["CreatedAt"]} | " +
                            $"{reader["Username"]} | " +
                            $"{reader["Actiontext"]}");
                    }
                }
            }
        }
    }
}
