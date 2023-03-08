using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    public class AdminFooterComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
