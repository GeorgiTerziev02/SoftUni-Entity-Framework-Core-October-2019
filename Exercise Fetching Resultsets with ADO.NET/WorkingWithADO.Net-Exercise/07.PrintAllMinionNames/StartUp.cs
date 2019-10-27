using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace _07.PrintAllMinionNames
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
            connection.Open();

            var names = new List<string>();

            using (connection)
            {
                var queryText = "SELECT Name FROM Minions";
                var command = new SqlCommand(queryText, connection);

                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        names.Add((string)reader["Name"]);
                    }
                }
            }

            while (names.Any())
            {
                Console.WriteLine(names[0]);
                names.RemoveAt(0);

                if (names.Any()==false)
                {
                    break;
                }

                Console.WriteLine(names[names.Count - 1]);
                names.RemoveAt(names.Count - 1);
            }
        }
    }
}
