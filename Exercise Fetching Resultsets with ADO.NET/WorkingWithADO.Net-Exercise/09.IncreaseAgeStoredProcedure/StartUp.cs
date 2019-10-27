using System;
using System.Data.SqlClient;

namespace _09.IncreaseAgeStoredProcedure
{
    class StartUp
    {
        private static string connectionString =
            "Server=.\\SQLEXPRESS;" +
            "Database=MinionsDb;" +
            "Integrated Security=true;";

        private static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            int inputId = int.Parse(Console.ReadLine());

            connection.Open();

            using (connection)
            {
                //var queryText = @"CREATE PROC usp_GetOlder @id INT
                //                    AS
                //                    UPDATE Minions
                //                    SET Age += 1
                //                    WHERE Id = @id";

                //SqlCommand command = new SqlCommand(queryText, connection);
                //command.ExecuteNonQuery();

                var queryText = "EXEC usp_GetOlder @Id";
                SqlCommand command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@Id", inputId);
                command.ExecuteNonQuery();

                queryText = "SELECT Name, Age FROM Minions WHERE Id = @Id";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@Id", inputId);

                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} – {reader["Age"]} years old");
                    }
                }
            }
        }
    }
}
