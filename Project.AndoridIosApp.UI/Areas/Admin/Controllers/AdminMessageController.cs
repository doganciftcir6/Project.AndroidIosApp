using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.SupportDtos;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminMessage/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class AdminMessageController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly IValidator<CreateMessageModel> _createMessageModelValidator;
        private readonly IValidator<UpdateMessageModel> _updateMessageModelValidator;
        private readonly IMapper _mapper;
        private readonly IDeviceService _deviceService;
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminMessageController(ISupportService supportService, IValidator<CreateMessageModel> createMessageModelValidator, IValidator<UpdateMessageModel> updateMessageModelValidator, IMapper mapper, IDeviceService deviceService, IProjectUserService projectUserService, IHttpContextAccessor httpContextAccessor)
        {
            _supportService = supportService;
            _createMessageModelValidator = createMessageModelValidator;
            _updateMessageModelValidator = updateMessageModelValidator;
            _mapper = mapper;
            _deviceService = deviceService;
            _projectUserService = projectUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _supportService.GetAllWithDeviceAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var deviceResponse = await _deviceService.GetAllAsync();
            var model = new CreateMessageModel()
            {
                Devices = new SelectList(deviceResponse.Data, "Id", "DeviceName"),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateMessageModel createMessageModel)
        {
            var validationResult = _createMessageModelValidator.Validate(createMessageModel);
            if (validationResult.IsValid)
            {
                //login olmuş kişiyi bulmak
                var loginUserResponse = GetLoginUser.CreateInstance(_httpContextAccessor, _projectUserService).RunAsync();
                if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    ModelState.AddModelError("", loginUserResponse.Result.Meessage);
                    return View(createMessageModel);
                }
                var mappingDataDto = _mapper.Map<CreateSupportDto>(createMessageModel);
                mappingDataDto.ProjectUserId = loginUserResponse.Result.Data.Id;
                var response = await _supportService.InsertAsync(mappingDataDto);
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
            //seleclist kaybolmamalı
            var deviceResponse2 = await _deviceService.GetAllAsync();
            createMessageModel.Devices = new SelectList(deviceResponse2.Data, "Id", "DeviceName");
            return View(createMessageModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _supportService.GetByIdAsync<UpdateSupportDto>(id);
            if (response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            //var mappingModel = new UpdateMessageModel()
            //{
            //    Id = response.Data.Id,
            //    Title = response.Data.Title,
            //    Content = response.Data.Content,
            //    Sender = response.Data.Sender,
            //    Receiver = response.Data.Receiver,
            //    SenderName = response.Data.SenderName,
            //    ReceiverName = response.Data.ReceiverName,
            //    Status = response.Data.Status,
            //    ProjectUserId = response.Data.ProjectUserId,
            //    DeviceId = response.Data.DeviceId,
            //};
            var mappingModel = _mapper.Map<UpdateMessageModel>(response.Data);
            //seleclist kaybolmamalı
            var deviceResponse3 = await _deviceService.GetAllAsync();
            mappingModel.Device = new SelectList(deviceResponse3.Data, "Id", "DeviceName");
            return View(mappingModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateMessageModel updateMessageModel)
        {
            var validationResult = _updateMessageModelValidator.Validate(updateMessageModel);
            if (validationResult.IsValid)
            {
                var mappingDto = _mapper.Map<UpdateSupportDto>(updateMessageModel);
                //login olmuş kişiyi bulmak
                var loginUserResponse = GetLoginUser.CreateInstance(_httpContextAccessor, _projectUserService).RunAsync();
                if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    ModelState.AddModelError("", loginUserResponse.Result.Meessage);
                    return View(updateMessageModel);
                }
                mappingDto.ProjectUserId = loginUserResponse.Result.Data.Id;
                var response = await _supportService.UpdateAsync(mappingDto);
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
            //seleclist kaybolmamalı
            //seleclist kaybolmamalı
            var deviceResponse4 = await _deviceService.GetAllAsync();
            updateMessageModel.Device = new SelectList(deviceResponse4.Data, "Id", "DeviceName");
            return View(updateMessageModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _supportService.DeleteAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
