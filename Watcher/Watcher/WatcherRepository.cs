//using Npgsql;
//using System;

//class WatcherRepository
//{
//    private ConnectionDB _db = new ConnectionDB();

//    public void InsertMetrics(float cpu, float ram, float hdd, string users)
//    {
//        try
//        {
//            using (var conn = _db.GetConnection())
//            using (var cmd = new NpgsqlCommand(
//                @"INSERT INTO systemstats 
//                (cpuusage, ramusage, hddusage, activeuser)
//                VALUES (@c, @r, @h, @u)", conn))
//            {
//                cmd.Parameters.AddWithValue("c", cpu);
//                cmd.Parameters.AddWithValue("r", ram);
//                cmd.Parameters.AddWithValue("h", hdd);
//                cmd.Parameters.AddWithValue("u", users);

//                cmd.ExecuteNonQuery();
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("InsertMetrics error: " + ex.Message);
//        }
//    }

//}