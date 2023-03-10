using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Helpers;
using Project.AndoridIosApp.UI.Models;
using Project.AndoridIosApp.UI.ValidationRules;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System.IO;
using System;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndoridIosApp.UI.Areas.Admin.Models;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProjectUser/{action}/{id?}")]
    public class AdminProjectUserController : Controller
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateModelValidator;
        private readonly IValidator<UpdateProjectUserModel> _updateProjectModelValidator;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminProjectUserController(IProjectUserService projectUserService, IGenderService genderService, IValidator<UserCreateModel> userCreateModelValidator, IMapper mapper, IHostingEnvironment hostingEnvironment, IValidator<UpdateProjectUserModel> updateProjectModelValidator)
        {
            _projectUserService = projectUserService;
            _genderService = genderService;
            _userCreateModelValidator = userCreateModelValidator;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _updateProjectModelValidator = updateProjectModelValidator;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _projectUserService.GetAllWithGenderAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel()
            {
                Genders = new SelectList(response.Data, "Id", "Definition")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(UserCreateModel userCreateModel)
        {
            var validation = _userCreateModelValidator.Validate(userCreateModel);
            if (validation.IsValid)
            {
                var dto = new CreateProjectUserDto();

                if (userCreateModel.ImageUrl != null)
                {
                    var imageRuleChecks = UserImageUploadRuleHelper.Run
                    (
                        UserCreateUploadCheckHelper.CheckImageName(userCreateModel.ImageUrl.FileName),
                        UserCreateUploadCheckHelper.CheckIfImageExtensionsAllow(userCreateModel.ImageUrl.FileName),
                        UserCreateUploadCheckHelper.CheckIfImageSizeIsLessThanOneMb(userCreateModel.ImageUrl.Length)
                    );
                    if (imageRuleChecks.ResponseType == ResponseType.Success)
                    {
                        //upload burada olacak çünkü ıformfile modelde verdim.
                        var fileName = Guid.NewGuid().ToString();
                        var extName = Path.GetExtension(userCreateModel.ImageUrl.FileName);
                        string path = Path.Combine(_hostingEnvironment.WebRootPath, "userImage", fileName + extName);
                        var stream = new FileStream(path, FileMode.Create);
                        await userCreateModel.ImageUrl.CopyToAsync(stream);
                        dto.ImageUrl = fileName + extName;
                    }
                    else
                    {
                        ModelState.AddModelError("", imageRuleChecks.Meessage);
                        //checklerda hata var hata mesajı gitsin ama aynı azamanda genderlist kaybolmamalı
                        var response3 = await _genderService.GetAllAsync();
                        userCreateModel.Genders = new SelectList(response3.Data, "Id", "Definition", userCreateModel.GenderId);
                        return View(userCreateModel);
                    }
                }

                dto.Username = userCreateModel.Username;
                dto.Firstname = userCreateModel.Firstname;
                dto.Lastname = userCreateModel.Lastname;
                dto.Password = userCreateModel.Password;
                dto.PasswordVerify = userCreateModel.PasswordVerify;
                dto.PhoneNumber = userCreateModel.PhoneNumber;
                dto.Email = userCreateModel.Email;
                dto.GenderId = userCreateModel.GenderId;

                var data = await _projectUserService.InsertWithRoleAsync(dto, (int)RoleType.Member);
                if (data.ResponseType == ResponseType.Error)
                {
                    ModelState.AddModelError("", data.Meessage);
                    var response2 = await _genderService.GetAllAsync();
                    userCreateModel.Genders = new SelectList(response2.Data, "Id", "Definition", userCreateModel.GenderId);
                    return View(userCreateModel);
                }
                else if (data.ResponseType == ResponseType.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            foreach (var item in validation.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            userCreateModel.Genders = new SelectList(response.Data, "Id", "Definition", userCreateModel.GenderId);
            return View(userCreateModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var genderResponse = await _genderService.GetAllAsync();
            var userResponse = await _projectUserService.GetByIdAsync<UpdateProjectUserDto>(id);
            if(userResponse.ResponseType == ResponseType.Success)
            {
                var model = new UpdateProjectUserModel()
                {
                    Id = userResponse.Data.Id,
                    Username = userResponse.Data.Username,
                    Firstname = userResponse.Data.Firstname,
                    Lastname = userResponse.Data.Lastname,
                    Password = userResponse.Data.Password,
                    PasswordVerify = userResponse.Data.PasswordVerify,
                    PhoneNumber = userResponse.Data.PhoneNumber,
                    Email = userResponse.Data.Email,
                    ImageUrl = userResponse.Data.ImageUrl,
                    GenderId = userResponse.Data.GenderId,
                    Genders = new SelectList(genderResponse.Data, "Id", "Definition")
                };
                return View(model);
            }
            return NotFound(userResponse.Meessage);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectUserModel updateProjectUserModel, IFormFile imageUrl)
        {
            var validation = _updateProjectModelValidator.Validate(updateProjectUserModel);
            if (validation.IsValid)
            {
                var dto = new UpdateProjectUserDto();

                if (imageUrl != null)
                {
                    var imageRuleChecks = UserImageUploadRuleHelper.Run
                    (
                        UserCreateUploadCheckHelper.CheckImageName(imageUrl.FileName),
                        UserCreateUploadCheckHelper.CheckIfImageExtensionsAllow(imageUrl.FileName),
                        UserCreateUploadCheckHelper.CheckIfImageSizeIsLessThanOneMb(imageUrl.Length)
                    );
                    if (imageRuleChecks.ResponseType == ResponseType.Success)
                    {
                        //upload burada olacak çünkü ıformfile modelde verdim.
                        var fileName = Guid.NewGuid().ToString();
                        var extName = Path.GetExtension(imageUrl.FileName);
                        string path = Path.Combine(_hostingEnvironment.WebRootPath, "userImage", fileName + extName);
                        var stream = new FileStream(path, FileMode.Create);
                        await imageUrl.CopyToAsync(stream);
                        updateProjectUserModel.ImageUrl = fileName + extName;
                    }
                    else
                    {
                        ModelState.AddModelError("", imageRuleChecks.Meessage);
                        //checklerda hata var hata mesajı gitsin ama aynı azamanda genderlist kaybolmamalı
                        var response3 = await _genderService.GetAllAsync();
                        updateProjectUserModel.Genders = new SelectList(response3.Data, "Id", "Definition", updateProjectUserModel.GenderId);
                        return View(updateProjectUserModel);
                    }
                }

                //update
                dto.Id = updateProjectUserModel.Id;
                dto.Username = updateProjectUserModel.Username;
                dto.Firstname = updateProjectUserModel.Firstname;
                dto.Lastname = updateProjectUserModel.Lastname;
                dto.Password = updateProjectUserModel.Password;
                dto.PasswordVerify = updateProjectUserModel.PasswordVerify;
                dto.PhoneNumber = updateProjectUserModel.PhoneNumber;
                dto.Email = updateProjectUserModel.Email;
                dto.GenderId = updateProjectUserModel.GenderId;
                dto.ImageUrl = updateProjectUserModel.ImageUrl;

                var data = await _projectUserService.UpdateAsync(dto);
                if (data.ResponseType == ResponseType.NotFound)
                {
                    ModelState.AddModelError("", data.Meessage);
                    var response2 = await _genderService.GetAllAsync();
                    updateProjectUserModel.Genders = new SelectList(response2.Data, "Id", "Definition", updateProjectUserModel.GenderId);
                    return View(updateProjectUserModel);
                }
                else if (data.ResponseType == ResponseType.Success)
                {
                    return RedirectToAction("Index");
                }
            }
            foreach (var item in validation.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var response = await _genderService.GetAllAsync();
            updateProjectUserModel.Genders = new SelectList(response.Data, "Id", "Definition", updateProjectUserModel.GenderId);
            return View(updateProjectUserModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _projectUserService.DeleteAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
