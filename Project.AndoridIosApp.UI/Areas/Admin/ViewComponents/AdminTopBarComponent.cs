using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    public class AdminTopBarComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
