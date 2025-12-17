using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using TempMailX.Models;

namespace TempMailX.Data
{
    public class TempEmailDAL
    {
        private readonly string _connection;

        public TempEmailDAL(string connection)
        {
            _connection = connection;
            CreateTable();
        }

        private void CreateTable()
        {
            using var con = new SqliteConnection(_connection);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = """
                CREATE TABLE IF NOT EXISTS TempEmails (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    EmailAddress TEXT,
                    CreatedAt TEXT,
                    ExpiryAt TEXT,
                    IsActive INTEGER
                );
            """;
            cmd.ExecuteNonQuery();
        }

        //  Save Email
        public void SaveEmail(string email)
        {
            using var con = new SqliteConnection(_connection);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = @"
            INSERT INTO TempEmails (EmailAddress, CreatedAt, ExpiryAt, IsActive)
            VALUES ($email, $created, $expiry, 1);";

            cmd.Parameters.AddWithValue("$email", email);
            cmd.Parameters.AddWithValue("$created", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("$expiry", DateTime.Now.AddMinutes(10).ToString());

            cmd.ExecuteNonQuery();
        }

        //  Used by MailController (Inbox)
        public List<TempEmail> GetActiveEmails()
        {
            var list = new List<TempEmail>();

            using var con = new SqliteConnection(_connection);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM TempEmails WHERE IsActive = 1";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new TempEmail
                {
                    Id = reader.GetInt32(0),
                    EmailAddress = reader.GetString(1),
                    CreatedAt = DateTime.Parse(reader.GetString(2)),
                    ExpiryAt = DateTime.Parse(reader.GetString(3)),
                    IsActive = reader.GetInt32(4) == 1
                });
            }

            return list;
        }

        //  Used by MailController (Deactivate)
        public void DeactivateEmail(int id)
        {
            using var con = new SqliteConnection(_connection);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE TempEmails SET IsActive = 0 WHERE Id = $id";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
        }

        // Used by Background Service
        public void ExpireOldEmails()
        {
            using var con = new SqliteConnection(_connection);
            con.Open();

            var cmd = con.CreateCommand();
            cmd.CommandText = @"
            UPDATE TempEmails
            SET IsActive = 0
            WHERE datetime(ExpiryAt) <= datetime('now')";
            cmd.ExecuteNonQuery();
        }
    }
}
