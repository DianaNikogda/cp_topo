using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    public class NoteService
    {
        private ConnectionDB _db = new ConnectionDB();
        public int AddNote(string text)
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("Требуется авторизация");
                return -1;
            }

            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(@"INSERT INTO Notes (UserId, Text) VALUES (@uid, @text) RETURNING Id", conn);
                cmd.Parameters.AddWithValue("uid", Session.CurrentUser.Id);
                cmd.Parameters.AddWithValue("text", text);
                return (int)cmd.ExecuteScalar();
            }
        }

        public void GetAllNotes()
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("Требуется авторизация");
                return;
            }

            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(@"SELECT Id, Text, CreatedAt FROM Notes WHERE UserId=@uid", conn);
                cmd.Parameters.AddWithValue("uid", Session.CurrentUser.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"ID: {reader["Id"]} | {reader["CreatedAt"]}\n{reader["Text"]}\n");
                    }
                }
            }
        }

        public void GetNoteById(int id)
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("Требуется авторизация");
                return;
            }

            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(@"SELECT Text, CreatedAt FROM Notes WHERE Id=@id AND UserId=@uid", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("uid", Session.CurrentUser.Id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine($"[{reader["CreatedAt"]}]\n{reader["Text"]}");
                    }
                    else
                    {
                        Console.WriteLine("Заметка не найдена");
                    }
                }
            }
        }

        public void DeleteNote(int id)
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("Требуется авторизация");
                return;
            }

            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(@"DELETE FROM Notes WHERE Id=@id AND UserId=@uid", conn);
                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("uid", Session.CurrentUser.Id);

                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Заметка удалена" : "Заметка не найдена");
            }
        }

        public void UpdateNote(int id, string newText)
        {
            if (Session.CurrentUser == null)
            {
                Console.WriteLine("Требуется авторизация");
                return;
            }

            using (var conn = _db.GetConnection())
            {
                var cmd = new NpgsqlCommand(@"UPDATE Notes SET Text=@text WHERE Id=@id AND UserId=@uid", conn);

                cmd.Parameters.AddWithValue("id", id);
                cmd.Parameters.AddWithValue("uid", Session.CurrentUser.Id);
                cmd.Parameters.AddWithValue("text", newText);

                int rows = cmd.ExecuteNonQuery();

                Console.WriteLine(rows > 0 ? "Заметка обновлена" : "Замтека не найдена");
            }
        }
    }
}
