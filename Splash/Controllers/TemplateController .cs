using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Splash.Models;

namespace Splash.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
    }
}
