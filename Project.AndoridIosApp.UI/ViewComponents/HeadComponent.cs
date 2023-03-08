using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class HeadComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
