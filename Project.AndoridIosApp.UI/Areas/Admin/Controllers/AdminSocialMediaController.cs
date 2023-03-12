using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.SocialMediaDtos;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminSocialMedia/{action}/{id?}")]
    public class AdminSocialMediaController : Controller
    {
        private readonly ISocialMediaService _socialMediaSevice;

        public AdminSocialMediaController(ISocialMediaService socialMediaSevice)
        {
            _socialMediaSevice = socialMediaSevice;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _socialMediaSevice.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateSocialMediaDto createSocialMediaDto)
        {
            var response = await _socialMediaSevice.InsertAsync(createSocialMediaDto);
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
                return View(createSocialMediaDto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _socialMediaSevice.GetByIdAsync<UpdateSocialMediaDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateSocialMediaDto updateSocialMediaDto)
        {
            var response = await _socialMediaSevice.UpdateAsync(updateSocialMediaDto);
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
                return View(updateSocialMediaDto);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _socialMediaSevice.DeleteAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
