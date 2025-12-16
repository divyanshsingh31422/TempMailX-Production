using Microsoft.Data.SqlClient;
using TempMailX.Models;

namespace TempMailX.Data
{
    public class TempEmailDAL
    {
        string cs =
        "Data Source=(LocalDB)\\MSSQLLocalDB;Integrated Security=True";

        public void SaveEmail(string email)
        {
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string q = @"INSERT INTO dbo.TempEmails
             (EmailAddress, CreatedAt, ExpiryAt, IsActive)
             VALUES (@e, GETDATE(), DATEADD(minute,30,GETDATE()), 1)";


            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.ExecuteNonQuery();
        }
        public List<TempEmail> GetActiveEmails()
        {
            List<TempEmail> list = new List<TempEmail>();

            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string q = "SELECT * FROM TempEmails WHERE IsActive = 1";
            SqlCommand cmd = new SqlCommand(q, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new TempEmail
                {
                    Id = (int)dr["Id"],
                    EmailAddress = dr["EmailAddress"].ToString(),
                    CreatedAt = (DateTime)dr["CreatedAt"],
                    ExpiryAt = (DateTime)dr["ExpiryAt"],
                    IsActive = (bool)dr["IsActive"]
                });
            }
            return list;
        }
        public void DeactivateEmail(int id)
        {
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string q = "UPDATE TempEmails SET IsActive = 0 WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
        }
        public void ExpireOldEmails()
        {
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string q = @"
        UPDATE TempEmails
        SET IsActive = 0
        WHERE IsActive = 1 AND ExpiryAt < GETDATE()";

            SqlCommand cmd = new SqlCommand(q, conn);
            cmd.ExecuteNonQuery();
        }





    }
}
