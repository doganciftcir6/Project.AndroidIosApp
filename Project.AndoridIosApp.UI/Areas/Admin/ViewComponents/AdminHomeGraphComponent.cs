using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    public class AdminHomeGraphComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
