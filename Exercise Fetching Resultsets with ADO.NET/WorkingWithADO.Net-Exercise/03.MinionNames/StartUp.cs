using System;
using System.Data.SqlClient;

namespace _03.MinionNames
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

            int inputId = int.Parse(Console.ReadLine());

            using (connection)
            {
                string queryText = $"SELECT Name FROM Villains WHERE Id = {inputId}";

                SqlCommand command = new SqlCommand(queryText, connection);

                string villainName = (string)command.ExecuteScalar();

                if (villainName == null)
                {
                    Console.WriteLine($"No villain with ID {inputId} exists in the database.");
                    return;
                }

                queryText = @$"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = {inputId}
                                ORDER BY m.Name";
                command = new SqlCommand(queryText, connection);
                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine($"Villain: {villainName}");
                using (reader)
                {
                    if (reader.HasRows == false)
                    {
                        Console.WriteLine("(no minions)");
                        return;
                    }

                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["RowNum"]}. {reader["Name"]} {reader["Age"]}");
                    }
                }
            }
        }
    }
}
