namespace AdoNetTest
{
    using System;
    using System.Data.SqlClient;
    class Program
    {
        static void Main(string[] args)
        {
            Func<int, int, int> LocalFunc = (int x, int y) => x + y;

            var someClass = new SomeClass();
            var connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=SoftUni;Integrated Security=True;");

            var name = "Kevin";

            connection.Open();

            using (connection)
            {
                var command = new SqlCommand("SELECT * FROM Employees WHERE FirstName = @name", connection);

                command.Parameters.AddWithValue("@name", name);
                //var result = command.ExecuteScalar();

                //Console.WriteLine(result);

                var reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var firstName = reader["FirstName"]; //1
                        var secondName = reader["LastName"]; //2
                        var title = reader["JobTitle"]; //4

                        var result = firstName + " " + secondName + " Job: " + title;

                        Console.WriteLine(result);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader[i] + " ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            //var town = new Town
            //{
            //    Name = "Sofia"
            //};

            ////custom
            //db.SaveChanges();

            //var towns = db
            //    .Towns
            //    .Where(t => t.Name = "Sofia")
            //    .ToList();

        }
    }
}
