using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splash.Models;

namespace Splash.Controllers
{
	public class IndexController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[Route("/ValidateUser")]
		public IActionResult ValidateUser([FromBody] LoginViewModel user)
		{
			if (user.username != "michell")
				return Json(new { isValid = false });

            if (user.password != "12345")
                return Json(new { isValid = false });


            return Json(new { isValid = true });
		}
    }
}
