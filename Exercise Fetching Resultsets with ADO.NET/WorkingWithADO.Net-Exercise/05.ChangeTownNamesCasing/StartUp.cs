using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace _05.ChangeTownNamesCasing
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
            string country = Console.ReadLine();
            connection.Open();

            using (connection)
            {
                string queryText = @" SELECT t.Name 
                                 FROM Towns as t
                                 JOIN Countries AS c ON c.Id = t.CountryCode
                                WHERE c.Name = @countryName";
                SqlCommand command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@countryName", country);
                SqlDataReader reader = command.ExecuteReader();

                List<string> townsBeforeChange = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        townsBeforeChange.Add((string)reader["Name"]);
                    }
                }

                queryText = @"UPDATE Towns
                                     SET Name = UPPER(Name)
                                     WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@countryName", country);
                int affectedRows = command.ExecuteNonQuery();

                queryText = @" SELECT t.Name 
                                 FROM Towns as t
                                 JOIN Countries AS c ON c.Id = t.CountryCode
                                WHERE c.Name = @countryName";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@countryName", country);
                reader = command.ExecuteReader();

                List<string> townsAfterChange = new List<string>();

                using (reader)
                {
                    while (reader.Read())
                    {
                        townsAfterChange.Add((string)reader["Name"]);
                    }
                }

                List<string> changedTowns = new List<string>();

                for (int i = 0; i <= townsBeforeChange.Count - 1; i++)
                {
                    if (townsBeforeChange[i] != townsAfterChange[i])
                    {
                        changedTowns.Add(townsAfterChange[i]);
                    }
                }


                if (changedTowns.Count == 0 || affectedRows == 0)
                {
                    Console.WriteLine("No town names were affected.");
                    return;
                }

                Console.WriteLine($"{affectedRows} town names were affected.");
                Console.WriteLine("["+string.Join(", ", changedTowns) +"]");
            }
        }
    }
}
