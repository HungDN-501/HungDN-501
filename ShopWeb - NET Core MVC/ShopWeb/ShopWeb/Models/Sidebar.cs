using Microsoft.AspNetCore.Mvc;
using ShopWeb.Data;

namespace ShopWeb.Models
{
	// Kế thừa thành ViewComponent
	public class Sidebar : ViewComponent
	{
		// ----- DB
		private readonly ShopWebContext _context;

		public Sidebar(ShopWebContext context)
		{
			_context = context;
		}

		// Trả về View file Default.cshtml ở folder Components/Sidebar
		public IViewComponentResult Invoke()
		{
			// trả về list category
			return View(_context.Category.ToList());
		}
	}
}
