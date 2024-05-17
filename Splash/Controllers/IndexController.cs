using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splash._Utilities;
using Splash.Models;
using System.Data.SqlClient;

namespace Splash.Controllers
{
    public class IndexController : Controller
    {
        private readonly IConfiguration _configuration;

        public IndexController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/ValidateUser")]
        public IActionResult ValidateUser([FromBody] LoginViewModel user)
        {

            string query = "select * from usuarios where usuario= '" + user.username + "'";

            //DatabaseConnector connector = new DatabaseConnector(_configuration);
            //connector.ConnectToAzureSQL();

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string hashedPassword = PasswordHasher.HashPassword(user.password);
                            //while (reader.Read())
                            //{
                            string senha_db = reader.GetString(reader.GetOrdinal("senha"));
                            if (senha_db != hashedPassword)
                                return Json(new { isValid = false });
                            //}
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return Json(new { isValid = true });
        }
    }
}
