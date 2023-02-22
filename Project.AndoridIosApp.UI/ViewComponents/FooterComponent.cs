using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class FooterComponent : ViewComponent
    {
        private readonly ISocialMediaService _socialMediaService;

        public FooterComponent(ISocialMediaService socialMediaService)
        {
            _socialMediaService = socialMediaService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _socialMediaService.GetAllAsync();
            return View(response.Data);
        }
    }
}
