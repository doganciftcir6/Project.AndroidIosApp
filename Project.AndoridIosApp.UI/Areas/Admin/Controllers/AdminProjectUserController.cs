using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Project.AndoridIosApp.UI.Helpers.UserHelper;

namespace Project.AndroidIosApp.Core.Helpers.UploadImageHelper
{
    [Area("Admin")]
    [Route("Admin/AdminProjectUser/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
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
            //update yaparken upload mapping hatası burasıyla ilgili çekilen var olan verilerde yani dtodaki stringi modeldeki ıformfile'a dönüştüremiyor bu yüzden upload sayfası hiç gelmiyor. Çözüm için modeldeki ımageurlede string yapıp dosyası update parametresinde yakalayacağız.
            var genderResponse = await _genderService.GetAllAsync();
            var userResponse = await _projectUserService.GetByIdAsync<UpdateProjectUserDto>(id);
            if (userResponse.ResponseType == ResponseType.Success)
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
        public async Task<IActionResult> Update(UpdateProjectUserModel updateProjectUserModel, IFormFile imageUrl, int id)
        {
            var loadedDtoData = await _projectUserService.GetByIdAsync<UpdateProjectUserDto>(id);
            if (loadedDtoData.ResponseType == ResponseType.Success)
            {
                //Direkt ımageurl null olarak algılanıp validation hatasına giriyor parametre olarak dosya adlığımız için. Ayrıca resim güncellenmek istemezse diye null kontrolü koymam lazım yoksa diğer bilgiler güncellenip dosya kısmı güncellenmezse burası null referance verir.
                if (imageUrl != null)
                {
                    updateProjectUserModel.ImageUrl = imageUrl.FileName;
                }
                else
                {
                    //eğer kullanıcı diğer bilgileri güncelleyip upload bilgisini güncellemediyse eski upload dosya bilgisini dtodan alıp modele vereceğiz ki bu sefer resim alanı boş diye validation'a takılmayalım bunun için parametre olarak id almaya ihtiyacım var ki getbyid ile var olan veriyi çekebileyim.
                    updateProjectUserModel.ImageUrl = loadedDtoData.Data.ImageUrl;
                }
            }
            else
            {
                return NotFound();
            }

            var validation = _updateProjectModelValidator.Validate(updateProjectUserModel);
            if (validation.IsValid)
            {
                var dto = new UpdateProjectUserDto();

                if (imageUrl != null)
                {
                    var uploadResponse = await UserImageUploadHelper.CreateInstance(_hostingEnvironment).RunUploadAsync(imageUrl);
                    if (uploadResponse.ResponseType == ResponseType.Success)
                    {
                        //veriyi dataresponseuma stringdata tanımlayarak onunla taşıyorum. Bu data Entitiy olmadığı için. String bir veri taşıyorum..
                        updateProjectUserModel.ImageUrl = uploadResponse.StringData;
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
                        updateProjectUserModel.Genders = new SelectList(genderList1.Data, "Id", "Definition");
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
            var value = await _projectUserService.GetByIdAsync<GetProjectUserDto>(id);
            if(value.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            var response = await _projectUserService.DeleteAsync(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }

            //upload edilmiş dosyalarıda kayıt ile birlikte serverdan silmem lazım.
            DeleteFileRun(value.Data.ImageUrl);
            return RedirectToAction("Index");
        }
        private void DeleteFileRun(string file)
        {
            try
            {
                var fileName = Path.GetFileNameWithoutExtension(file);
                var extName = Path.GetExtension(file);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "userImage", fileName + extName);
                if (path.Contains(fileName))
                {
                    System.IO.File.Delete(path);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
