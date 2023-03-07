using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult Error404()
        {
            return View();
        }
    }
}
