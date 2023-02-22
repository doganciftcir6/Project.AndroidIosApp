using Microsoft.AspNetCore.Mvc;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class CarouselCompenent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
