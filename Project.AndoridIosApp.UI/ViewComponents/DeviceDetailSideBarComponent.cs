using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class DeviceDetailSideBarComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public DeviceDetailSideBarComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _blogService.GetAllBySortingToCreateDateAsync();
            return View(result.Data);
        }
    }
}
