using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeProgressBarOSComponent : ViewComponent
    {
        private readonly IDeviceService _deviceService;

        public AdminHomeProgressBarOSComponent(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var iosDevices = await _deviceService.GetAllIosAsync();
            var androidDevices = await _deviceService.GetAllAndoridAsync();

            ViewBag.IosDevicesCount = iosDevices.Data.Count;
            ViewBag.AndroidDevicesCount = androidDevices.Data.Count;
            return View();
        }
    }
}
