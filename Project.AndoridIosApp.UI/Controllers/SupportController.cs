using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.DataAccess.Contexts.EntityFramework;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Dtos.SupportDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class SupportController : Controller
    {
        private readonly ISupportService _supportService;
        private readonly IProjectUserService _projectUserService; 
        private readonly IDeviceService _deviceService;
        private readonly IValidator<SendMessageModel> _sendMessageModelValidator;
        private readonly AndroidIosContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMapper _mapper;

        public SupportController(ISupportService supportService, IHttpContextAccessor contextAccessor, IProjectUserService projectUserService, IDeviceService deviceService, IValidator<SendMessageModel> sendMessageModelValidator, AndroidIosContext context, IMapper mapper)
        {
            _supportService = supportService;
            _contextAccessor = contextAccessor;
            _projectUserService = projectUserService;
            _deviceService = deviceService;
            _sendMessageModelValidator = sendMessageModelValidator;
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult> ReceiverMessage()
        {
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_contextAccessor, _projectUserService).RunAsync();
            if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", loginUserResponse.Result.Meessage);
                return View(loginUserResponse.Result.Data);
            }
            var loginUserMail = loginUserResponse.Result.Data.Email;
            //bu şekilde maile göre listeleme yapıcaz kullanıcının mailine göre.
            var messageList = await _supportService.GetAllByEmailReceiverAsync(loginUserMail);
  
            if(messageList.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", messageList.Meessage);
                return View(messageList.Data);
            }
            return View(messageList.Data);
        }
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult> SenderMessage()
        {
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_contextAccessor, _projectUserService).RunAsync();
            if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", loginUserResponse.Result.Meessage);
                return View(loginUserResponse.Result.Data);
            }
            var loginUserMail = loginUserResponse.Result.Data.Email;
            ////bu şekilde maile göre listeleme yapıcaz kullanıcının mailine göre.
            var messageList = await _supportService.GetAllByEmailSenderAsync(loginUserMail);

            if (messageList.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", messageList.Meessage);
                return View(messageList.Data);
            }
            return View(messageList.Data);
        }
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult> ReceiverMessageDetails(int id)
        {
            var result = await _supportService.GetByIdWithUserAsync(id);
            if(result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", result.Meessage);
                return View(result.Data);
            }
            return View(result.Data);
        }
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult>SenderMessageDetails(int id)
        {
            var result = await _supportService.GetByIdWithUserAsync(id);
            if(result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", result.Meessage);
                return View(result.Data);
            }
            return View(result.Data);
        }
        [HttpGet]
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult> SendMessage()
        {
            var response = await _deviceService.GetAllAsync();
            var model = new SendMessageModel()
            {
                Devices = new SelectList(response.Data,"Id", "DeviceName")
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Member, SupportUser")]
        public async Task<IActionResult> SendMessage(SendMessageModel sendMessageModel)
        {
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_contextAccessor, _projectUserService).RunAsync();
            if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                ModelState.AddModelError("", loginUserResponse.Result.Meessage);
                var response = await _deviceService.GetAllAsync();
                //selectlist kaybolmasın
                sendMessageModel.Devices = new SelectList(response.Data, "Id", "DeviceName", sendMessageModel.DeviceId);
                return View(sendMessageModel);
            }
            
            var validation = _sendMessageModelValidator.Validate(sendMessageModel);
            if (validation.IsValid)
            {
                var loginUserMail = loginUserResponse.Result.Data.Email;
                //login olan kullanıcının map işlemleri
                sendMessageModel.Sender = loginUserMail;
                sendMessageModel.SenderName = loginUserResponse.Result.Data.Firstname + " " + loginUserResponse.Result.Data.Lastname;
                //date
                sendMessageModel.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //login olan kullanıcının project user ıd
                sendMessageModel.ProjectUserId = loginUserResponse.Result.Data.Id;
                //mesaj gönderirken alıcı adının eklenmesi
                var usernameSurname = await _context.ProjectUsers.Where(x => x.Email == sendMessageModel.Receiver).Select(x => x.Firstname + " " + x.Lastname).FirstOrDefaultAsync();
                sendMessageModel.ReceiverName = usernameSurname;
                //model mapping model to dto
                var dto = _mapper.Map<CreateSupportDto>(sendMessageModel);
                var result = await _supportService.InsertAsync(dto);
                if(result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Success)
                {
                    return RedirectToAction("SenderMessage");
                }
            }
            foreach (var item in validation.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //device selectlist kaybolmasın
            var response2 = await _deviceService.GetAllAsync();
            //selectlist kaybolmasın
            sendMessageModel.Devices = new SelectList(response2.Data, "Id", "DeviceName", sendMessageModel.DeviceId);
            return View(sendMessageModel);
        }
    }
}
