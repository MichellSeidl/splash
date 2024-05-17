using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Splash.DAO
{
    public class DatabaseConnector
    {
        private readonly IConfiguration _configuration;

        public DatabaseConnector(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConnectToAzureSQL()
        {
            string? azureConnectionString = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");

            using (SqlConnection connection = new SqlConnection(azureConnectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to Azure SQL Database opened successfully.");
                    // You can perform database operations here
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Handle the exception appropriately
                }
            }
        }
    }
}
