using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        // Cargar los datos compartidos en ViewData
        ViewData["Username"] = HttpContext.Session.GetString("Username");
        ViewData["Role"] = HttpContext.Session.GetString("Role");
        ViewData["UserId"] = HttpContext.Session.GetString("UserId");


        Console.WriteLine(HttpContext.Session.GetString("UserRole") + "BASE");
    }
}