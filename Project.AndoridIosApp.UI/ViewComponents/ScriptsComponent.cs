using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class ScriptsComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
