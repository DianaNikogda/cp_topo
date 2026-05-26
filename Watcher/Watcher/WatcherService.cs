using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading;

class WatcherService
{
    private SystemMetricsService metrics = new SystemMetricsService();
    private ConnectionDB db = new ConnectionDB();
   

    public void SaveMetrics()
    {
        int cpu = metrics.GetCpuCores();
        float ram = metrics.GetRamUsedMB();
        float hdd = metrics.GetDiskUsage();
        string os = metrics.GetOS();
        string machine = metrics.GetMachineName();
        string users = GetUsers();

        using (var conn = db.GetConnection())
        {
            string sql = @"
                INSERT INTO systemstats
                (cpucores, ramusedmb, hddusage, os, machinename, activeuser)
                VALUES
                (@cpu, @ram, @hdd, @os, @machine, @users)";

            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("cpu", cpu);
                cmd.Parameters.AddWithValue("ram", ram);
                cmd.Parameters.AddWithValue("hdd", hdd);
                cmd.Parameters.AddWithValue("os", os);
                cmd.Parameters.AddWithValue("machine", machine);
                cmd.Parameters.AddWithValue("users", users);

                cmd.ExecuteNonQuery();
            }
        }

        Console.WriteLine("Metrics saved");
    }

    public void ShowStats()
    {
        using (var conn = db.GetConnection())
        {
            string sql = "SELECT * FROM systemstats ORDER BY createdat DESC LIMIT 10";

            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(
                        $"{reader["createdat"]} | CPU cores: {reader["cpucores"]} | RAM: {reader["ramusedmb"]} MB | HDD: {reader["hddusage"]}% | OS: {reader["os"]} | Machine: {reader["machinename"]}"
                    );
                }
            }
        }
    }
    public void AutoMode()
    {
        Console.WriteLine("Авто режим запущен");

        while (true)
        {
            SaveMetrics();

            Thread.Sleep(60000);
        }
    }
    public string GetUsers()
    {
        
            using (var conn = db.GetConnection())
            {
                string sql =
                    "SELECT DISTINCT username FROM logs WHERE createdat > NOW() - INTERVAL '10 minutes'";

                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    var users = new System.Collections.Generic.List<string>();

                    while (reader.Read())
                        users.Add(reader["username"].ToString());

                    return users.Count == 0 ? "none" : string.Join(", ", users);
                }
            }
    }
       
}
