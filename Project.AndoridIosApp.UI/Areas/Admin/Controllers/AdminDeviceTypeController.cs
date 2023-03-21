using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminDeviceType/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class AdminDeviceTypeController : Controller
    {
        private readonly IDeviceTypeService _deviceTypeService;

        public AdminDeviceTypeController(IDeviceTypeService deviceTypeService)
        {
            _deviceTypeService = deviceTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _deviceTypeService.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateDeviceTypeDto createDeviceTypeDto)
        {
            var response = await _deviceTypeService.InsertAsync(createDeviceTypeDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(createDeviceTypeDto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _deviceTypeService.GetByIdAsync<UpdateDeviceTypeDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateDeviceTypeDto updateDeviceTypeDto)
        {
            var response = await _deviceTypeService.UpdateAsync(updateDeviceTypeDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(updateDeviceTypeDto);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _deviceTypeService.DeleteAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
