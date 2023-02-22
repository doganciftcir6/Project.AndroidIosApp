using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Linq;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class RecentlyAddedComponent : ViewComponent
    {
        private readonly IDeviceService _deviceService;

        public RecentlyAddedComponent(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await _deviceService.GetAllBySortingToCreateDateWithOsDeviceTypeAsync();
            return View(response.Data);
        }
    }
}
