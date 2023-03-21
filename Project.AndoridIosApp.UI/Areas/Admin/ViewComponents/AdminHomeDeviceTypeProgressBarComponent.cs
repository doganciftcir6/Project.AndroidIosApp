using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeDeviceTypeProgressBarComponent : ViewComponent
    {
        private readonly IDeviceService _deviceService;

        public AdminHomeDeviceTypeProgressBarComponent(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var phoneDevices = await _deviceService.GetAllPhoneAsync();
            var tabletDevices = await _deviceService.GetAllTabletAsync();

            ViewBag.PhoneDevicesCount = phoneDevices.Data.Count;
            ViewBag.TabletDevicesCount = tabletDevices.Data.Count;
            return View();
        }
    }
}
