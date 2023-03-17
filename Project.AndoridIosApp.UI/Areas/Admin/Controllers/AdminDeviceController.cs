using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using X.PagedList;
using Project.AndroidIosApp.DataAccess.Abstract.Repositories;
using AutoMapper;
using FluentValidation;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Core.Helpers.UploadImageHelper;
using Project.AndoridIosApp.UI.Helpers.DeviceHelper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.AndroidIosApp.Dtos.BlogDtos;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminDevice/{action}/{id?}")]
    public class AdminDeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly IValidator<CreateDeviceModel> _createDeviceModelValidator;
        private readonly IValidator<UpdateDeviceModel> _updateDeviceModelValidator;
        private readonly IMapper _mapper;
        private readonly IDeviceTypeService _deviceTypeService;
        private readonly IOSService _OSservice;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AdminDeviceController(IDeviceService deviceService, IValidator<CreateDeviceModel> createDeviceModelValidator, IValidator<UpdateDeviceModel> updateDeviceModelValidator, IMapper mapper, IDeviceTypeService deviceTypeService, IOSService oSservice, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _deviceService = deviceService;
            _createDeviceModelValidator = createDeviceModelValidator;
            _updateDeviceModelValidator = updateDeviceModelValidator;
            _mapper = mapper;
            _deviceTypeService = deviceTypeService;
            _OSservice = oSservice;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var response = await _deviceService.GetAllWithOSAndDeviceTypeAsync();
            var data = response.Data.ToPagedList(page, 7);
            return View(data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var deviceTypeResponse = await _deviceTypeService.GetAllAsync();
            var deviceOSResponse = await _OSservice.GetAllAsync();
            var model = new CreateDeviceModel()
            {
                DeviceType = new SelectList(deviceTypeResponse.Data, "Id", "Definition"),
                OS = new SelectList(deviceOSResponse.Data, "Id", "Definition"),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateDeviceModel createDeviceModel)
        {
            var validationResult = _createDeviceModelValidator.Validate(createDeviceModel);
            if (validationResult.IsValid)
            {
                var mappingDataDto = _mapper.Map<CreateDeviceDto>(createDeviceModel);
                //upload
                if (createDeviceModel.ImageUrl != null)
                {
                    var uploadResponse = DeviceImageUploadThenAferwwrootHelper.CreateInstance(_hostingEnvironment).RunDeviceUploadAsync(createDeviceModel.ImageUrl);
                    if(uploadResponse.Result.ResponseType == ResponseType.Success)
                    {
                        //data yerine message kullandım veri string olduğu için message olarak algılıyor entity olarak değil.
                        mappingDataDto.ImageUrl = uploadResponse.Result.Meessage;
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
                        //checklerda hata var hata mesajı gitsin ama aynı azamanda seleclist kaybolmamalı
                        var deviceTypeResponse6 = await _deviceTypeService.GetAllAsync();
                        var deviceOSResponse6 = await _OSservice.GetAllAsync();
                        createDeviceModel.DeviceType = new SelectList(deviceTypeResponse6.Data, "Id", "Definition");
                        createDeviceModel.OS = new SelectList(deviceOSResponse6.Data, "Id", "Definition");
                        return View(createDeviceModel);
                    }
                }
                var response = await _deviceService.InsertAsync(mappingDataDto);
                if (response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var deviceTypeResponse2 = await _deviceTypeService.GetAllAsync();
            var deviceOSResponse2 = await _OSservice.GetAllAsync();
            createDeviceModel.DeviceType = new SelectList(deviceTypeResponse2.Data, "Id", "Definition");
            createDeviceModel.OS = new SelectList(deviceOSResponse2.Data, "Id", "Definition");
            return View(createDeviceModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            //update yaparken upload mapping hatası burasıyla ilgili çekilen var olan verilerde yani dtodaki stringi modeldeki ıformfile'a dönüştüremiyor bu yüzden upload sayfası hiç gelmiyor. Çözüm için modeldeki ımageurlede string yapıp dosyası update parametresinde yakalayacağız.
            var response = await _deviceService.GetByIdAsync<UpdateDeviceDto>(id);
            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound(response.Meessage);
            }
            var deviceTypeResponse4 = await _deviceTypeService.GetAllAsync();
            var deviceOSResponse4 = await _OSservice.GetAllAsync();
            var mappingModel = _mapper.Map<UpdateDeviceModel>(response.Data);
            mappingModel.DeviceType = new SelectList(deviceTypeResponse4.Data, "Id", "Definition");
            mappingModel.OS = new SelectList(deviceOSResponse4.Data, "Id", "Definition");
            return View(mappingModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateDeviceModel updateDeviceModel, IFormFile ImageUrl, int id)
        {
            var loadedDtoData = await _deviceService.GetByIdAsync<UpdateDeviceDto>(id);
            if (loadedDtoData.ResponseType == ResponseType.Success)
            {
                //Direkt ımageurl null olarak algılanıp validation hatasına giriyor parametre olarak dosya adlığımız için. Ayrıca resim güncellenmek istemezse diye null kontrolü koymam lazım yoksa diğer bilgiler güncellenip dosya kısmı güncellenmezse burası null referance verir.
                if (ImageUrl != null)
                {
                    updateDeviceModel.ImageUrl = ImageUrl.FileName;
                }
                else
                {
                    //eğer kullanıcı diğer bilgileri güncelleyip upload bilgisini güncellemediyse eski upload dosya bilgisini dtodan alıp modele vereceğiz ki bu sefer resim alanı boş diye validation'a takılmayalım bunun için parametre olarak id almaya ihtiyacım var ki getbyid ile var olan veriyi çekebileyim.
                    updateDeviceModel.ImageUrl = loadedDtoData.Data.ImageUrl;
                }
            }
            else
            {
                NotFound();
            }

            var mappingDto = _mapper.Map<UpdateDeviceDto>(updateDeviceModel);
            var validationResult = _updateDeviceModelValidator.Validate(updateDeviceModel);
            if (validationResult.IsValid)
            {
                //pathtteki wwwroottan sonrasını dbye kaydeden upload
                if (ImageUrl != null)
                {
                    var uploadResponse = DeviceImageUploadThenAferwwrootHelper.CreateInstance(_hostingEnvironment).RunDeviceUploadAsync(ImageUrl);
                    if(uploadResponse.Result.ResponseType == ResponseType.Success)
                    {
                        //data messagede geliyor. Karıştırma!
                        mappingDto.ImageUrl = uploadResponse.Result.Meessage;
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
                        //checklerda hata var hata mesajı gitsin ama aynı azamanda seleclist kaybolmamalı
                        var deviceTypeResponse6 = await _deviceTypeService.GetAllAsync();
                        var deviceOSResponse6 = await _OSservice.GetAllAsync();
                        updateDeviceModel.DeviceType = new SelectList(deviceTypeResponse6.Data, "Id", "Definition");
                        updateDeviceModel.OS = new SelectList(deviceOSResponse6.Data, "Id", "Definition");
                        return View(updateDeviceModel);
                    }
                }
                var updateResponse = await _deviceService.UpdateAsync(mappingDto);
                if (updateResponse.ResponseType == ResponseType.NotFound)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            var deviceTypeResponse5 = await _deviceTypeService.GetAllAsync();
            var deviceOSResponse5 = await _OSservice.GetAllAsync();
            updateDeviceModel.DeviceType = new SelectList(deviceTypeResponse5.Data, "Id", "Definition");
            updateDeviceModel.OS = new SelectList(deviceOSResponse5.Data, "Id", "Definition");
            return View(updateDeviceModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResponse = await _deviceService.DeleteAsync(id);
            if (deleteResponse.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}

