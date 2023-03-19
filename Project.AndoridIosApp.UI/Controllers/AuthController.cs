using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Helpers.UploadImageHelper;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using System.Linq;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateModelValidator;
        private readonly IValidator<UserUpdateModel> _userUpdateModelValidator;
        private readonly IProjectUserService _projectUserService;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;
        public AuthController(IGenderService genderService, IValidator<UserCreateModel> userCreateModelValidator, IProjectUserService projectUserService, IMapper mapper, IHostingEnvironment hostingEnvironment, IValidator<UserUpdateModel> userUpdateModelValidator)
        {
            _genderService = genderService;
            _userCreateModelValidator = userCreateModelValidator;
            _projectUserService = projectUserService;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
            _userUpdateModelValidator = userUpdateModelValidator;
        }

        public async Task<IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel()
            {
                Genders = new SelectList(response.Data, "Id", "Definition")
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel userCreateModel)
        {
            var validation = _userCreateModelValidator.Validate(userCreateModel);
            if (validation.IsValid)
            {
                var dto = new CreateProjectUserDto();

                if (userCreateModel.ImageUrl != null)
                {
                    var uploadResponse = UserImageUploadHelper.CreateInstance(_hostingEnvironment).RunUploadAsync(userCreateModel.ImageUrl);
                    if (uploadResponse.Result.ResponseType == ResponseType.Success)
                    {
                        //veriyi dataresponseuma stringdata tanımlayarak onunla taşıyorum. Bu data Entitiy olmadığı için. String bir veri taşıyorum..
                        dto.ImageUrl = uploadResponse.Result.StringData;
                    }
                    else
                    {
                        //hata mesajlarını birbirinden ayıralım ki alta alta gelsinler.
                        //mesajların sonuna ünlemden sonra ^ yazdırdım ki ! gitmesin.
                        var errorMessages = uploadResponse.Result.Meessage.Split('^');
                        foreach (var errorMessage in errorMessages)
                        {
                            ModelState.AddModelError("", errorMessage);
                        }
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
                    return RedirectToAction("SignIn");
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
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginProjectUserDto loginProjectUserDto)
        {
            var result = await _projectUserService.CheckUserAsync(loginProjectUserDto);
            if (result.ResponseType == ResponseType.Success)
            {
                var roleInfo = await _projectUserService.GetRolesByUserIdAsync(result.Data.Id);
                var claims = new List<Claim>();
                if (roleInfo.ResponseType == ResponseType.Success)
                {
                    foreach (var item in roleInfo.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Definition));
                    }
                }
                claims.Add(new Claim(ClaimTypes.Name, result.Data.Username));
                var claimsIdentity = new ClaimsIdentity(
                   claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = loginProjectUserDto.RememberMe,
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);
                return RedirectToAction("Index", "Home");
            }
            //login işlemi gerçekleşmez
            ModelState.AddModelError("", result.Meessage);
            return View(loginProjectUserDto);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(int id)
        {
            var response = await _projectUserService.GetByIdAsync<UpdateProjectUserDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            var mappingDataModel = _mapper.Map<UserUpdateModel>(response.Data);
            var genderList = await _genderService.GetAllAsync();
            mappingDataModel.Genders = new SelectList(genderList.Data, "Id", "Definition");
            return View(mappingDataModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UserUpdateModel userUpdateModel, int id, IFormFile ImageUrl)
        {
            //kullanıcı upload dosyayını güncellemeden güncelleme yaparsa varolan veri kaybolmamalı;
            var oldData = await _projectUserService.GetByIdAsync<UpdateProjectUserDto>(id);
            if(oldData.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            if(ImageUrl != null)
            {
                //bu sayede kullanıcı upload validationlarına girdiğinde value inputunda validationa takılan yeni dosyanın ismi gözükecek.
                userUpdateModel.ImageUrl = ImageUrl.FileName;
            }
            else
            {
                userUpdateModel.ImageUrl = oldData.Data.ImageUrl;
            }

            var validationResult = _userUpdateModelValidator.Validate(userUpdateModel);
            if (validationResult.IsValid)
            {
                //upload
                if (ImageUrl != null)
                {
                    var uploadResponse = await UserImageUploadHelper.CreateInstance(_hostingEnvironment).RunUploadAsync(ImageUrl);
                    if (uploadResponse.ResponseType == ResponseType.Success)
                    {
                        //veriyi dataresponseuma stringdata tanımlayarak onunla taşıyorum. Bu data Entitiy olmadığı için. String bir veri taşıyorum..
                        userUpdateModel.ImageUrl = uploadResponse.StringData;
                    }
                    else
                    {
                        //hata mesajlarını birbirinden ayıralım ki alta alta gelsinler.
                        //mesajların sonuna ünlemden sonra ^ yazdırdım ki ! gitmesin.
                        var errorMessages = uploadResponse.Meessage.Split('^');
                        foreach (var errorMessage in errorMessages)
                        {
                            ModelState.AddModelError("", errorMessage);
                        }
                        //checklerda hata var hata mesajı gitsin ama aynı azamanda seleclist kaybolmamalı
                        var genderList1 = await _genderService.GetAllAsync();
                        userUpdateModel.Genders = new SelectList(genderList1.Data, "Id", "Definition");
                        return View(userUpdateModel);
                    }
                }

                var mappingDto = _mapper.Map<UpdateProjectUserDto>(userUpdateModel);
                var updateResponse = await _projectUserService.UpdateAsync(mappingDto);
                if (updateResponse.ResponseType == ResponseType.NotFound)
                {
                    return NotFound(updateResponse.Meessage);
                }
                //kulanıcıyı update işleminden sonra çıkış yaptıralım ki tekrar giriş yapıp verilerini cookiede güncellemiş olsun.
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("SignIn");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //selectlist kaybolmamalı
            var genderList = await _genderService.GetAllAsync();
            userUpdateModel.Genders = new SelectList(genderList.Data, "Id", "Definition");
            return View(userUpdateModel);

        }
    }
}
