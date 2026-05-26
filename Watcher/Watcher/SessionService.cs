//using Npgsql;
//using System;
//using System.Collections.Generic;

//class SessionService
//{
//    private ConnectionDB _db = new ConnectionDB();

//    public List<string> GetActiveUsers()
//    {
//        var users = new List<string>();

//        try
//        {
//            using (var conn = _db.GetConnection())
//            using (var cmd = new NpgsqlCommand("SELECT DISTINCT Username FROM Logs WHERE CreatedAt > NOW() - INTERVAL '10 minutes'", conn))
//            using (var reader = cmd.ExecuteReader())
//            {
//                while (reader.Read())
//                    users.Add(reader["Username"].ToString());
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine("SessionService error: " + ex.Message);
//        }

//        return users;
//    }
//}