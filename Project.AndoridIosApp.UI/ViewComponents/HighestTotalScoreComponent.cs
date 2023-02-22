using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class HighestTotalScoreComponent : ViewComponent
    {
        private readonly IDeviceService _deviceService;

        public HighestTotalScoreComponent(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _deviceService.GetAllBySortingToTotalScoreWithOsDeviceTypeAsync();
            return View(result.Data);
        }
    }
}
