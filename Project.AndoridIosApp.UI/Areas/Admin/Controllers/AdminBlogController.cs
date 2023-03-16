using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.BlogDtos;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;
using X.PagedList.Web.Common;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBlog/{action}/{id?}")]
    public class AdminBlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminBlogController(IBlogService blogService, IHostingEnvironment hostingEnvironment)
        {
            _blogService = blogService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var blogResponse = await _blogService.GetAllAsync();
            var pagedData = blogResponse.Data.ToPagedList(page,7);
            return View(pagedData);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            //view'a dto göndermezsem radiobutonlar null hatası verecektir.
            return View(new CreateBlogDto());
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateBlogDto createBlogDto, IFormFile Image1, IFormFile Image2, IFormFile Image3)
        {
            var insertBlogResponse = await _blogService.InsertAsync(createBlogDto, Image1, Image2, Image3);
            if(insertBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(insertBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Error)
            {
                //hata mesajlarını birbirinden ayıralım ki alta alta gelsinler.
                var errorMessages = insertBlogResponse.Meessage.Split('!');
                foreach (var errorMessage in errorMessages)
                {
                    ModelState.AddModelError("", errorMessage);
                }
                return View(createBlogDto);
            }
            else if(insertBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in insertBlogResponse.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(createBlogDto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _blogService.GetByIdAsync<UpdateBlogDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound(response.Meessage);
            }
            return View(response.Data);
        }
        [HttpPost]
        public  async Task<IActionResult> Update(UpdateBlogDto updateBlogDto, IFormFile Image1, IFormFile Image2, IFormFile Image3, int id)
        {
            var updateBlogResponse = await _blogService.UpdateAsync(updateBlogDto, Image1, Image2, Image3, id);
            if(updateBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(updateBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Error)
            {
                //hata mesajlarını birbirinden ayıralım ki alta alta gelsinler.
                var errorMessages = updateBlogResponse.Meessage.Split('!');
                foreach (var errorMessage in errorMessages)
                {
                    ModelState.AddModelError("", errorMessage);
                }
                return View(updateBlogDto);
            }
            else if(updateBlogResponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in updateBlogResponse.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(updateBlogDto);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteReponse = await _blogService.DeleteAsync(id);
            if(deleteReponse.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
