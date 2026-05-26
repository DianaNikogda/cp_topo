using Npgsql;

class ConnectionDB
{
    private string connectionString =
         "Host=localhost;Port=5432;Username=watcher;Password=watcherwatcher;Database=topo";

    public NpgsqlConnection GetConnection()
    {
        NpgsqlConnection conn = new NpgsqlConnection(connectionString);
        conn.Open();
        return conn;
    }
}