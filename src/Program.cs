using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace CsvToSqlServer
{
    class Program
    {
        static void Main()
        {
            string csvFilePath = "test.csv";
            string connectionString = "Data Source=localhost;Initial Catalog=master;User ID=sa;Password=3qoLxoCRw7;";
            string dataLoadConnectionString = "Data Source=localhost;Initial Catalog=CsvToSqlServerDatabase;User ID=DataLoadUser;Password=3qoLxoCRw7;";

            try
            {
                // Read CSV file
                List<Customer> dataRecords = ReadCsvFile(csvFilePath);


                // Insert data into MSSQL database
                InsertDataIntoDatabase(dataRecords, dataLoadConnectionString);

                Console.WriteLine("Data successfully loaded into the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static List<Customer> ReadCsvFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                return csv.GetRecords<Customer>().ToList();
            }
        }

        static void createDatabaseAndTables(string connectionString)
        {
            runSqlScript("schema.sql",connectionString);
        }

        static void runSqlScript(string scriptFilePath,string connectionString)
        {
            try
            {
                // Read the SQL script from the file
                string sqlScript = File.ReadAllText(scriptFilePath);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create a command object
                    using (SqlCommand command = new SqlCommand(sqlScript, connection))
                    {
                        // Execute the SQL script
                        command.ExecuteNonQuery();
                    }

                    Console.WriteLine("SQL script executed successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void InsertDataIntoDatabase(List<Customer> dataRecords, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                foreach (var record in dataRecords)
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO CsvToSqlServer.Customer (forename, surname, postcode) VALUES (@Value1, @Value2, @Value3)", connection))
                    {
                        cmd.Parameters.AddWithValue("@Value1", record.forename);
                        cmd.Parameters.AddWithValue("@Value2", record.surname);
                        cmd.Parameters.AddWithValue("@Value3", record.postcode);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}