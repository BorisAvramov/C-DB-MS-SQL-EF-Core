
namespace ADONET
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection.Metadata;
    using System.Reflection.Metadata.Ecma335;
    using Microsoft.Data.SqlClient;


    public class Program
    {
        
        public static void Main(string[] args)
        {

            // connetct to sqlServer on my PC - "Server=DESKTOP-6ADIQGK\SQLEXPRESS; Integrated Security=true; Database=NationalTouristSitesOfBulgaria; TrustServerCertificate = True"

            // connect to docker container sql server User Id=sa;Password=avramov!87;
            //const string sqlConnectionString = "Server=localhost; User Id=sa; Password=avramov!87; Database=MinonsDB; TrustServerCertificate = True";
            const string sqlConnectionString = "Server=DESKTOP-6ADIQGK\\SQLEXPRESS; Integrated Security=true; Database=MinonsDB; TrustServerCertificate = True";

            SqlConnection connection = new SqlConnection(sqlConnectionString);

            //connection.Open();
            //connection.Close();

            using (connection)
            {
                connection.Open();

                // 1 exer.
                //InitialSetup(connection);

                // 2 exerc.
                //GetVallainsNames(connection);

                // 3 exerc.
                //GetMinionsNames(connection);

                // 4 exerc.

                //AddMinion(connection);

                // 5. exerc.

                ChangeTownNamesCasing(connection);



            }


        }

        private static void ChangeTownNamesCasing(SqlConnection connection)
        {
            string country = Console.ReadLine();


            List<string> coutryTowns = GetCountryTowns(connection, country).ToList();

            if (coutryTowns.Count > 0)
            {
                UpdateTownsToUpper(connection, country);

                Console.WriteLine($"[{string.Join(", ", coutryTowns)}]");

            }
        }

        private static void UpdateTownsToUpper(SqlConnection connection, string country)
        {
            using (var commandUdateTownsToUpper = new SqlCommand("UPDATE Towns SET Name = UPPER(Name) WHERE (SELECT Id FROM Countries WHERE Name = @NameContry) = Towns.CountryCode", connection))
            {
                commandUdateTownsToUpper.Parameters.AddWithValue("@NameContry", country);

                int rowsAffected = commandUdateTownsToUpper.ExecuteNonQuery();

                //commandUdateTownsToUpper.Dispose();

                Console.WriteLine($"{rowsAffected} town names were affected. ");
            }
        }

        private static List<string> GetCountryTowns(SqlConnection connection, string country)
        {
            List<string> countryTowns = new List<string>();

             using (var commandSelectTowns = new SqlCommand("SELECT t.Name FROM Countries C JOIN Towns T ON T.CountryCode = C.Id WHERE C.Name = @CountryName", connection))
            {
                var townNames = new List<string>();

                commandSelectTowns.Parameters.AddWithValue("@CountryName", country);

                var reader = commandSelectTowns.ExecuteReader();

                if (!reader.HasRows)
                {
                    Console.WriteLine("No town names were affected.");

                }
                else
                {

                    using (reader)
                    {
                        while (reader.Read())
                        {

                            countryTowns.Add((string)reader[0]);

                        }

                    }
                }


            }
            return countryTowns;
        }

        private static void AddMinion(SqlConnection connection)
        {
            string[] minionsInfo = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);


            string nameMin = minionsInfo[1];
            int ageMin = int.Parse(minionsInfo[2]);
            string townNameMin = minionsInfo[3];

            string[] villainsInfo = Console.ReadLine().Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);



            string nameVillain = villainsInfo[1];




            object townId = GetTownId(connection, townNameMin);

            if (townId == null)
            {
                AddTownToDB(connection, townNameMin);


                townId = GetTownId(connection, townNameMin);
            }



            InsertTownIdInMinion(connection, (int)townId, nameMin);

            object villainId = GetVillainId(connection, nameVillain);

            if (villainId == null)
            {
                AddVillain(connection, nameVillain);

                villainId = GetVillainId(connection, nameVillain);

            }



            AddMInionToVillain(connection, nameMin, nameVillain, (int)villainId);
        }

        private static object GetVillainId(SqlConnection connection, string nameVillain)
        {
            using (var commandSelectVillain = new SqlCommand("SELECT Id FROM Villains WHERE Name = @Name", connection))
            {
                commandSelectVillain.Parameters.AddWithValue("@Name", nameVillain);
                var idVillain = commandSelectVillain.ExecuteScalar();
                return idVillain;

            }


            throw new NotImplementedException();
        }

        private static object GetTownId(SqlConnection connection, string townNameMin)
        {
            
            using (var commandSelectIdTown = new SqlCommand("SELECT Id FROM TOWNS WHERE Name = @Name", connection))
            {
                commandSelectIdTown.Parameters.AddWithValue("@Name", townNameMin);
                var townId = commandSelectIdTown.ExecuteScalar();
                return townId;
            }
        }

        private static void AddMInionToVillain(SqlConnection connection, string nameMin, string nameVillain, int villainId)
        {
            using (var commandSelectMinId = new SqlCommand("SELECT Id FROM Minions WHERE Name = @Name", connection))
            {
                commandSelectMinId.Parameters.AddWithValue("@Name", nameMin);

                var idMinion = commandSelectMinId.ExecuteScalar();

                using (var commandAddMinionToVillain = new SqlCommand("INSERT INTO MinionsVillains VALUES(@MinionId, @VillainId)", connection))
                {
                    commandAddMinionToVillain.Parameters.AddWithValue("MinionId", idMinion);
                    commandAddMinionToVillain.Parameters.AddWithValue("VillainId", villainId);

                    commandAddMinionToVillain.ExecuteNonQuery();

                    Console.WriteLine($"Successfully added {nameMin} to be minion of {nameVillain}.");


                }




            }
        }

        private static void InsertTownIdInMinion(SqlConnection connection, int townId, string nameMin)
        {
            using (var commandInsertTownIdInMinion = new SqlCommand("UPDATE Minions SET TownId=@townId WHERE Name=@Name", connection))
            {
                commandInsertTownIdInMinion.Parameters.AddWithValue("@townId", townId);
                commandInsertTownIdInMinion.Parameters.AddWithValue("@Name", nameMin);

                commandInsertTownIdInMinion.ExecuteNonQuery();




            }
        }


        private static void AddTownToDB(SqlConnection connection, string townNameMin)
        {
            using (var commandAddTownToDB = new SqlCommand("INSERT INTO Towns (Id, Name) VALUES (11, @Name)", connection))
            {

                commandAddTownToDB.Parameters.AddWithValue("@Name", townNameMin);

                commandAddTownToDB.ExecuteNonQuery();

                

                Console.WriteLine($"Town {townNameMin} was added to the database.");


            }
        }

   

        private static void AddVillain(SqlConnection connection, string nameVillain)
        {
            using (var commandAddVillain = new SqlCommand("INSERT INTO Villains VALUES (8, @Name, 4)", connection))
            {
                commandAddVillain.Parameters.AddWithValue("@Name", nameVillain);

                commandAddVillain.ExecuteNonQuery();

               

            }

            Console.WriteLine($"Villain {nameVillain} was added to the database.");
        }

        private static void GetMinionsNames(SqlConnection connection)
        {
            var vilId = int.Parse(Console.ReadLine());

            var cmdGetVillainById = "SELECT Name FROM Villains WHERE Id = @Id";

            using (var command = new SqlCommand(cmdGetVillainById, connection))
            {
                command.Parameters.AddWithValue("@Id", vilId);
                var nameVillain = command.ExecuteScalar();



                if (nameVillain == null)
                {
                    Console.WriteLine($"No villain with ID {vilId} exists in the database");
                }
                else
                {
                    var cmdStrMinions = $"SELECT M.Name, M.Age FROM MinionsVillains MV JOIN Minions M ON MV.MinionId = M.Id JOIN Villains V ON V.Id = MV.VillainId WHERE V.Id = {vilId}";
                    using (var commandGetMinions = new SqlCommand(cmdStrMinions, connection))
                    {

                        var reader = commandGetMinions.ExecuteReader();

                        using (reader)
                        {
                            var minionsList = new List<string>();
                            Console.WriteLine($"Villain: {nameVillain}");
                            var count = 1;
                            while (reader.Read())
                            {
                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("no minions");
                                }
                                else
                                {
                                    Console.WriteLine($"{count}. {reader[0]} {reader[1]}");
                                    count++;
                                }
                              

                            }


                        }

                    }
                }

            }
        }

        private static void GetVallainsNames(SqlConnection connection)
        {
            string selectQuery = @"SELECT V.Name, COUNT(*) FROM Villains V JOIN MinionsVillains MV ON V.Id = MV.VillainId GROUP BY V.Id, V.Name";
            using (var command = new SqlCommand(selectQuery, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                using (reader)
                {
                    while (reader.Read())
                    {
                        var name = reader[0];
                        var count = reader[1];

                        Console.WriteLine($"{name} - {count}");



                    }


                }


            }
        }

        private static void InitialSetup(SqlConnection connection)
        {
            //string strCommandCreateDatabase = "CREATE DATABASE MinonsDB";
            var createTableStatements = GetCreateTableStatments();


            foreach (var createTableQuery in createTableStatements)
            {

                GetCreateTables(createTableQuery, connection);

            }

            var insertTableStatments = GetInsertTableStatements();

            foreach (var insertTableQuery in insertTableStatments)
            {
                GetInsertTables(connection, insertTableQuery);
            }
        }

        private static void GetInsertTables(SqlConnection connection, string insertTableQuery)
        {
            using (var command = new SqlCommand (insertTableQuery, connection))
            {
                command.ExecuteNonQuery();

            }
        }

        private static void GetCreateTables( string createTaleCommand, SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand(createTaleCommand, connection))
            {

                command.ExecuteNonQuery();

            }
        }

        private static string[] GetInsertTableStatements()
        {
            var result = new string[]
            {
                "INSERT INTO Countries (Id, Name) VALUES (1, 'Bulgaria'), (2, 'Norway'), (3, 'Cyprus'), (4, 'Greece'), (5, 'UK')",
                "INSERT INTO TOWNS (Id, Name, CountryCode) VALUES (1, 'Plovdiv', 1), (2, 'Oslo', 2), (3, 'Larnaca', 3), (4, 'Athens', 4), (5, 'London', 5)",
                "INSERT INTO Minions VALUES (1, 'Stoyan', 12, 1), (2, 'George', 22, 2), (3, 'Ivan', 25, 3), (4, 'Kiro', 35, 4), (5, 'Niki', 25, 5)",
                "INSERT INTO EvilnessFactors VALUES (1, 'super good'), (2, 'good'), (3, 'bad'), (4, 'evil'), (5, 'super evil')",
                "INSERT INTO Villains VALUES (1, 'Gru', 1), (2, 'Ivo', 2), (3, 'Teo', 3), (4, 'Sto', 4), (5, 'Pro', 5)",
                "INSERT INTO MinionsVillains VALUES (1, 1), (2, 2), (3, 3), (4, 4)"
            };
            return result;
        }

        private static string [] GetCreateTableStatments()
        {
            var result = new string[]
            {
                "CREATE TABLE Countries(Id INT PRIMARY KEY, Name VARCHAR(50))",
                "CREATE TABLE Towns(Id INT PRIMARY KEY, Name VARCHAR(50), CountryCode INT FOREIGN KEY REFERENCES Countries(Id))",
                "CREATE TABLE Minions(Id INT PRIMARY KEY, Name VARCHAR(50), Age INT, TownId INT FOREIGN KEY REFERENCES Towns(Id))",
                "CREATE TABLE EvilnessFactors(Id INT PRIMARY KEY, Name VARCHAR(50))",
                 "CREATE TABLE Villains(Id INT PRIMARY KEY, Name VARCHAR(50), EvilnessFactorId INT FOREIGN KEY REFERENCES EvilnessFactors(Id))",
                 "CREATE TABLE MinionsVillains(MinionId INT FOREIGN KEY REFERENCES Minions(Id), VillainId INT FOREIGN KEY REFERENCES Villains(Id) CONSTRAINT PK_MinionsVillains PRIMARY KEY(MinionId, VillainId))"
            };

            return result;
        }
    }
}