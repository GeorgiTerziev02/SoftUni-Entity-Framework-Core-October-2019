using System;
using System.Data.SqlClient;

namespace _04.AddMinion
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
            //Minion: Bob 14 Berlin
            //Villain: Gru

            var minionsInput = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var villainInput = Console.ReadLine().Split(": ", StringSplitOptions.RemoveEmptyEntries);

            var minionName = minionsInput[1];
            var minionAge = int.Parse(minionsInput[2]);
            var minionTown = minionsInput[3];

            var villainName = villainInput[1];

            connection.Open();

            using (connection)
            {
                string queryText = "SELECT Id FROM Towns WHERE Name = @townName";
                SqlCommand command = new SqlCommand(queryText, connection);

                command.Parameters.AddWithValue("@townName", minionTown);

                var townId = command.ExecuteScalar();

                if (townId == null)
                {
                    queryText = "INSERT INTO Towns (Name) VALUES (@townName)";
                    command = new SqlCommand(queryText, connection);

                    command.Parameters.AddWithValue("@townName", minionTown);

                    command.ExecuteNonQuery();

                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                queryText = "SELECT Id FROM Villains WHERE Name = @Name";
                command = new SqlCommand(queryText, connection);

                command.Parameters.AddWithValue("@Name", villainName);

                var villainId = command.ExecuteScalar();

                if (villainId == null)
                {
                    queryText = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
                    command = new SqlCommand(queryText, connection);

                    command.Parameters.AddWithValue("@villainName", villainName);

                    command.ExecuteNonQuery();

                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                queryText = "SELECT Id FROM Towns WHERE Name = @townName";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@townName", minionTown);

                townId = command.ExecuteScalar();

                queryText = "INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";
                command = new SqlCommand(queryText, connection);

                command.Parameters.AddWithValue("@nam", minionName);
                command.Parameters.AddWithValue("@age", minionAge);
                command.Parameters.AddWithValue("@townId", (int)townId);

                command.ExecuteNonQuery();

                queryText = "SELECT Id FROM Minions WHERE Name = @Name";
                command = new SqlCommand(queryText, connection);

                command.Parameters.AddWithValue("@Name", minionName);

                var minionId = (int)command.ExecuteScalar();

                queryText = "SELECT Id FROM Villains WHERE Name = @Name";
                command = new SqlCommand(queryText, connection);

                command.Parameters.AddWithValue("@Name", villainName);

                var vilId = command.ExecuteScalar();

                queryText = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
                command = new SqlCommand(queryText, connection);
                command.Parameters.AddWithValue("@villainId", vilId);
                command.Parameters.AddWithValue("@minionId", minionId);

                Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
            }        
        }
    }
}
