using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SerMisDemo.Models
{
    public class PersonModel
    {
        public static void UpdateData(string name, string field, string value)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = $"IF EXISTS (SELECT 1 FROM People WHERE Name = @Name) " +
                               $"BEGIN " +
                               $"UPDATE People SET {field} = @Value, Flag = 1, Date_Updated = GETDATE() WHERE Name = @Name " +
                               $"END " +
                               $"ELSE " +
                               $"BEGIN " +
                               $"INSERT INTO People (Name, {field}, Flag, Date_Updated) VALUES (@Name, @Value, 1, GETDATE()) " +
                               $"END";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Value", value);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}