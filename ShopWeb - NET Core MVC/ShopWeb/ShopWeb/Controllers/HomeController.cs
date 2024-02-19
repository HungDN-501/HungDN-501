using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;
using ShopWeb.Models;
using System.Diagnostics;

namespace ShopWeb.Controllers
{
	public class HomeController : Controller
	{
		private readonly ShopWebContext _context;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ShopWebContext context, ILogger<HomeController> logger)
		{
			_context = context;
			_logger = logger;
		}

		public IActionResult Index()
		{
			// ----- trả về list Product
			return View(_context.Product?.ToList());
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}