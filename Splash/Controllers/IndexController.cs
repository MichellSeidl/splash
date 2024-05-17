using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Splash._Utilities;
using Splash.DAO;
using Splash.Models;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            string query = "select * from usuarios where usuario=@usuario";
            DatabaseConnector connector = new DatabaseConnector(_configuration);
            bool isValid = connector.ValidateLogin(query, user.username, PasswordHasher.HashPassword(user.password));
            
            return Json(new { isValid = isValid });
        }
    }
}
