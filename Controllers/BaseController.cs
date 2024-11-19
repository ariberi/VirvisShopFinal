using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        // Cargar los datos compartidos en ViewData
        ViewData["UserEmail"] = HttpContext.Session.GetString("UserEmail");
        ViewData["UserRole"] = HttpContext.Session.GetString("UserRole");

        Console.WriteLine(HttpContext.Session.GetString("UserRole") + "BASE");
    }
}
