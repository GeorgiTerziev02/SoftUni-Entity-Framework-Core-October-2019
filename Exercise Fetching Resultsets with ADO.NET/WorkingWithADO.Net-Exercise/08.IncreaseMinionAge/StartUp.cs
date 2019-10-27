using System;
using System.Linq;
using System.Data.SqlClient;

namespace _08.IncreaseMinionAge
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
            int[] inputNumbers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            connection.Open();

            using (connection)
            {
                var queryText = @" UPDATE Minions
                                    SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1
                                   WHERE Id = @Id";

                SqlCommand command = new SqlCommand(queryText, connection);

                for (int i = 0; i <= inputNumbers.Length - 1; i++)
                {
                    command.Parameters.AddWithValue("@Id", inputNumbers[i]);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                queryText = "SELECT Name, Age FROM Minions";

                command = new SqlCommand(queryText, connection);
                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]} {reader["Age"]}");
                    }
                }

            }
        }
    }
}
