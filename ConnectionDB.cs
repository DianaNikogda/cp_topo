using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CP_TOPO
{
    class ConnectionDB
    {
        private string _connectionString = "Host=localhost;Port=5432;Username=postgres;Password=diana21bir;Database=topo";

        public NpgsqlConnection GetConnection()
        {
            var conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            return conn;
        }
    }
}
