using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebBanHang.Models.Authentication
{
    public class Authentication : ActionFilterAttribute
    {
        // Kiểm tra nếu chưa đăng nhập thì ko đc vào 
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetString("UserName") == null)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        {"Controller", "Access" },
                        {"Action", "Login" }
                    }
                );
            }
        }
    }
}
