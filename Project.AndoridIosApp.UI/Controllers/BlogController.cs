using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCommentService _blogCommentService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProjectUserService _projectUserService;

        public BlogController(IBlogService blogService, IBlogCommentService blogCommentService, IHttpContextAccessor httpContextAccessor, IProjectUserService projectUserService)
        {
            _blogService = blogService;
            _blogCommentService = blogCommentService;
            _httpContextAccessor = httpContextAccessor;
            _projectUserService = projectUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _blogService.GetAllAsync();
            return View(response.Data);

        }
        public async Task<IActionResult> BlogDetails(int id)
        {
            var result = await _blogService.GetByIdWithProjectUserCommentAsync(id);
            var blogComment = await _blogCommentService.GetAllBlogCommentWithUserAsync(id);
            //login olmuş kişiyi bulmak
            var loginUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var loginUser = await _projectUserService.FindByUserNameAsync(loginUserName);
            if (loginUser.ResponseType == ResponseType.Success)
            {
                ViewBag.UserId = loginUser.Data.Id;
            }

            ViewBag.Id = id;
            ViewBag.BlogComment = blogComment.Data;
            ViewBag.CommentCount = blogComment.Data.Count();

            if (result.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View(new CreateBlogDto());
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateBlogDto dto)
        {
            var response = await _blogService.InsertAsync(dto);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            else if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(response.Data);
            }
                return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //return View(new UpdateBlogDto
            //{
            //    Id = data.Id,
            //    Subtitle = data.Subtitle,
            //    Description = data.Description,
            //    Company = data.Company,
            //    Image1 = data.Image1,
            //    Image2 = data.Image2,
            //    Image3 = data.Image3,
            //    CreateDate = data.CreateDate,
            //    Status = data.Status
            //});

            var response = await _blogService.GetByIdAsync<UpdateBlogDto>(id);
            if(response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogDto service)
        {
            var response = await _blogService.UpdateAsync(service);
            if(response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(response.Data);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _blogService.DeleteAsync(id);
            if(response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}

