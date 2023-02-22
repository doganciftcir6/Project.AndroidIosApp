using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class HeaderComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
