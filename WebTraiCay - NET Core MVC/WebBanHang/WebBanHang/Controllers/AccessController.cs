using Microsoft.AspNetCore.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class AccessController : Controller
    {
        QlbanValiContext db = new QlbanValiContext();

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserName") == null) 
            { 
                return View();
            }
            else
            {
                // chuyển đến action "Index" trong controller "Home"
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public IActionResult Login(TUser user)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                // kiểm tra thông tin người dùng
                var u = db.TUsers.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    // Set Session
                    HttpContext.Session.SetString("UserName", u.Username.ToString());
                    // chuyển đến action "Index" trong controller "Home"
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            // Xóa các Session
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            // chuyển đến action "Login" trong controller "Access"
            return RedirectToAction("Login", "Access");
        }
    }
}
