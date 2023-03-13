using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndoridIosApp.UI.Models;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.CommentDtos;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminDeviceComment/{action}/{id?}")]
    public class AdminDeviceComment : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IValidator<CreateDeviceCommentModel> _createDeviceCommentModelValidator;
        private readonly IValidator<UpdateDeviceCommentModel> _updateDeviceCommentModelValidator;
        private readonly IMapper _mapper;
        private readonly IProjectUserService _projectUserService;
        private readonly IDeviceService _deviceService;

        public AdminDeviceComment(ICommentService commentService, IValidator<CreateDeviceCommentModel> createDeviceCommentModelValidator, IMapper mapper, IProjectUserService projectUserService, IDeviceService deviceService, IValidator<UpdateDeviceCommentModel> updateDeviceCommentModelValidator)
        {
            _commentService = commentService;
            _createDeviceCommentModelValidator = createDeviceCommentModelValidator;
            _mapper = mapper;
            _projectUserService = projectUserService;
            _deviceService = deviceService;
            _updateDeviceCommentModelValidator = updateDeviceCommentModelValidator;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _commentService.GetAllCommentWithUserAndDeviceAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var projectUserResponse = await _projectUserService.GetAllAsync();
            var deviceResponse = await _deviceService.GetAllAsync();
            var model = new CreateDeviceCommentModel()
            {
                ProjectUsers = new SelectList(projectUserResponse.Data, "Id", "Username"),
                Devices = new SelectList(deviceResponse.Data, "Id", "DeviceName"),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateDeviceCommentModel createDeviceCommentModel)
        {
            var validation = _createDeviceCommentModelValidator.Validate(createDeviceCommentModel);
            if (validation.IsValid)
            {
                var mappingData = _mapper.Map<CreateCommentDto>(createDeviceCommentModel);
                var response = await _commentService.InsertCommentAsync(mappingData);
                if (response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validation.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //selectlistteki veriler validatoneror sonrasında kaybolmamalı ayrıca checkbutonda
            var projectUserResponse2 = await _projectUserService.GetAllAsync();
            var deviceResponse2 = await _deviceService.GetAllAsync();
            createDeviceCommentModel.ProjectUsers = new SelectList(projectUserResponse2.Data, "Id", "Username");
            createDeviceCommentModel.Devices = new SelectList(deviceResponse2.Data, "Id", "DeviceName");
            return View(createDeviceCommentModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _commentService.GetByIdAsync<UpdateCommentDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            //selectlisti göndermeliyiz var olan bilgiyi selectliste taşımak için ıdlerinide maplemeliyiz modele ayrıca map işlemleride yapılmalı.
            var projectUserResponse4 = await _projectUserService.GetAllAsync();
            var deviceResponse4 = await _deviceService.GetAllAsync();
            var model = new UpdateDeviceCommentModel()
            {
                ProjectUsers = new SelectList(projectUserResponse4.Data, "Id", "Username"),
                ProjectUserId = response.Data.ProjectUserId,
                Devices = new SelectList(deviceResponse4.Data, "Id", "DeviceName"),
                DeviceId = response.Data.DeviceId,
                Id = response.Data.Id,
                Content = response.Data.Content,
                Status = response.Data.Status,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateDeviceCommentModel updateDeviceCommentModel)
        {
            var validation = _updateDeviceCommentModelValidator.Validate(updateDeviceCommentModel);
            if (validation.IsValid)
            {
                var mappingData = _mapper.Map<UpdateCommentDto>(updateDeviceCommentModel);
                mappingData.UpdateDate = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                var response = await _commentService.UpdateCommentAsync(mappingData);
                if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
                {
                    NotFound();
                }
                return RedirectToAction("Index");
            }
            foreach (var item in validation.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            //selectlistteki veriler validatoneror sonrasında kaybolmamalı ayrıca checkbutonda kullandığım özellikler sayesinde o da kaybolmayacak.
            var projectUserResponse3 = await _projectUserService.GetAllAsync();
            var deviceResponse3 = await _deviceService.GetAllAsync();
            updateDeviceCommentModel.ProjectUsers = new SelectList(projectUserResponse3.Data, "Id", "Username");
            updateDeviceCommentModel.Devices = new SelectList(deviceResponse3.Data, "Id", "DeviceName");
            return View(updateDeviceCommentModel);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _commentService.DeleteCommentAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
