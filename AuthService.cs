using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
     class AuthService
    {
        private ConnectionDB _db = new ConnectionDB();
        HashService _hashService = new HashService();

        public bool Login(string username, string password)
        {
            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand("SELECT * FROM Users WHERE Username=@u", conn);

                cmd.Parameters.AddWithValue("u", username);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader["PasswordHash"].ToString();
                        string inputHash = _hashService.GetMD5(password);

                        if (storedHash == inputHash)
                        {
                            Session.CurrentUser = new User
                            {
                                Id = (int)reader["Id"],
                                Username = reader["Username"].ToString(),
                                Role = reader["Role"].ToString()
                            };

                            Console.WriteLine("Авторизация успешна!");
                            return true;
                        }
                    }
                }
            }

            Console.WriteLine("Введены неверные данные");
            return false;
        }

        public void Logout()
        {
            Session.CurrentUser = null;
            Console.WriteLine("Выход из системы");
        }

        public void Register(string username, string password)
        {
            //if (Session.CurrentUser == null || Session.CurrentUser.Role != "Admin")
            //{
            //    Console.WriteLine("Доступ запрещен");
            //    return;
            //}

            using (var conn = _db.GetConnection())
            {
                string hash = _hashService.GetMD5(password);

                var cmd = new NpgsqlCommand("INSERT INTO Users (Username, PasswordHash, Role) VALUES (@u, @p, 'User')",conn);

                cmd.Parameters.AddWithValue("u", username);
                cmd.Parameters.AddWithValue("p", hash);

                cmd.ExecuteNonQuery();
            }

            Console.WriteLine("Пользователь создан");
        }

      
    }
}
