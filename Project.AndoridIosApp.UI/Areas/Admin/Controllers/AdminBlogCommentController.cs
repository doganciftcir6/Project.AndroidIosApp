using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using System;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminBlogComment/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class AdminBlogCommentController : Controller
    {
        private readonly IBlogCommentService _blogCommentService;
        private readonly IValidator<CreateBlogCommentModel> _createBlogCommentModelValidator;
        private readonly IValidator<UpdateBlogCommentModel> _updateBlogCommentModelValidator;
        private readonly IMapper _mapper;
        private readonly IProjectUserService _projectUserService;
        private readonly IBlogService _blogService;

        public AdminBlogCommentController(IBlogCommentService blogCommentService, IValidator<CreateBlogCommentModel> createBlogCommentModelValidator, IValidator<UpdateBlogCommentModel> updateBlogCommentModelValidator, IMapper mapper, IProjectUserService projectUserService, IBlogService blogService)
        {
            _blogCommentService = blogCommentService;
            _createBlogCommentModelValidator = createBlogCommentModelValidator;
            _updateBlogCommentModelValidator = updateBlogCommentModelValidator;
            _mapper = mapper;
            _projectUserService = projectUserService;
            _blogService = blogService;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _blogCommentService.GetAllBlogCommentWithUserAndBlogAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var projectUserResponse = await _projectUserService.GetAllAsync();
            var blogResponse = await _blogService.GetAllAsync();
            var model = new CreateBlogCommentModel()
            {
                ProjectUsers = new SelectList(projectUserResponse.Data, "Id", "Username"),
                Blogs = new SelectList(blogResponse.Data, "Id", "Title")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateBlogCommentModel createBlogCommentModel)
        {
            var validationResult = _createBlogCommentModelValidator.Validate(createBlogCommentModel);
            if (validationResult.IsValid)
            {
                var mappingData = _mapper.Map<CreateBlogCommentDto>(createBlogCommentModel);
                var response = await _blogCommentService.InsertBlogCommentAsync(mappingData);
                if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //selectlist verileri kaybolmamalı
            var projectUserResponse2 = await _projectUserService.GetAllAsync();
            var blogResponse2 = await _blogService.GetAllAsync();
            createBlogCommentModel.ProjectUsers = new SelectList(projectUserResponse2.Data, "Id", "Username");
            createBlogCommentModel.Blogs = new SelectList(blogResponse2.Data, "Id", "Title");
            return View(createBlogCommentModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _blogCommentService.GetByIdAsync<UpdateBlogCommentDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            var mappingModel = _mapper.Map<UpdateBlogCommentModel>(response.Data);
            //selectlist seçili gelmeli
            var projectUserResponse3 = await _projectUserService.GetAllAsync();
            var blogResponse3 = await _blogService.GetAllAsync();
            mappingModel.ProjectUsers = new SelectList(projectUserResponse3.Data, "Id", "Username");
            mappingModel.Blogs = new SelectList(blogResponse3.Data, "Id", "Title");
            return View(mappingModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBlogCommentModel updateBlogCommentModel)
        {
            var validationResult = _updateBlogCommentModelValidator.Validate(updateBlogCommentModel);
            if (validationResult.IsValid)
            {
                var mappingData = _mapper.Map<UpdateBlogCommentDto>(updateBlogCommentModel);
                //updatedate güncellenmeli
                mappingData.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var response = await _blogCommentService.UpdateBlogCommentAsync(mappingData);
                if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //hatadan sonra selectlistler kaybolmamalı
            var projectUserResponse4 = await _projectUserService.GetAllAsync();
            var blogResponse4 = await _blogService.GetAllAsync();
            updateBlogCommentModel.ProjectUsers = new SelectList(projectUserResponse4.Data, "Id", "Username");
            updateBlogCommentModel.Blogs = new SelectList(blogResponse4.Data, "Id", "Title");
            return View(updateBlogCommentModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _blogCommentService.DeleteBlogCommentAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
