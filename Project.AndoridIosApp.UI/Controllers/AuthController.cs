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

namespace Project.AndoridIosApp.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IGenderService _genderService;
        private readonly IValidator<UserCreateModel> _userCreateModelValidator;
        private readonly IProjectUserService _projectUserService;
        private readonly IMapper _mapper;
        public AuthController(IGenderService genderService, IValidator<UserCreateModel> userCreateModelValidator, IProjectUserService projectUserService, IMapper mapper)
        {
            _genderService = genderService;
            _userCreateModelValidator = userCreateModelValidator;
            _projectUserService = projectUserService;
            _mapper = mapper;
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
                var dto = _mapper.Map<CreateProjectUserDto>(userCreateModel);
                var data = await _projectUserService.InsertWithRoleAsync(dto,(int)RoleType.Member);
                if(data.ResponseType == ResponseType.Error)
                {
                        ModelState.AddModelError("",data.Meessage);
                        var response2 = await _genderService.GetAllAsync();
                        userCreateModel.Genders = new SelectList(response2.Data, "Id", "Definition", userCreateModel.GenderId);
                        return View(userCreateModel);
                }
                else if(data.ResponseType == ResponseType.Success)
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
            if(result.ResponseType == ResponseType.Success)
            {
                var roleInfo = await _projectUserService.GetRolesByUserIdAsync(result.Data.Id);
                var claims = new List<Claim>();
                if(roleInfo.ResponseType == ResponseType.Success)
                {
                    foreach (var item in roleInfo.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Definition));
                    }
                }
                claims.Add(new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()));
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
            ModelState.AddModelError("Kulanıcı adı veya şifre hatalı", result.Meessage);
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
    }
}
