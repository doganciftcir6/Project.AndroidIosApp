using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
