using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminHome")]
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
