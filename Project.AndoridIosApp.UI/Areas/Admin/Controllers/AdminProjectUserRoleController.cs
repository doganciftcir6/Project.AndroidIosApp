using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndoridIosApp.UI.Areas.Admin.Models;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.ProjectUserRoleDto;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProjectUserRole/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class AdminProjectUserRoleController : Controller
    {
        private readonly IProjectUserRoleService _projectUserRoleService;
        private readonly IMapper _mapper;
        private readonly IProjectUserService _projectUserService;
        private readonly IProjectRoleService _projectRoleService;

        public AdminProjectUserRoleController(IProjectUserRoleService projectUserRoleService, IMapper mapper, IProjectUserService projectUserService, IProjectRoleService projectRoleService)
        {
            _projectUserRoleService = projectUserRoleService;
            _mapper = mapper;
            _projectUserService = projectUserService;
            _projectRoleService = projectRoleService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _projectUserRoleService.GetAllWithProjectUserAndRoleAsync();
            return View(response.Data);
        }
        [HttpGet]
        public async Task<IActionResult> Insert()
        {
            var projectUserResponse = await _projectUserService.GetAllAsync();
            var projectRoleResponse = await _projectRoleService.GetAllAsync();
            var model = new CreateProjectUserRoleModel()
            {
                ProjectUser = new SelectList(projectUserResponse.Data, "Id", "Username"),
                ProjectRole = new SelectList(projectRoleResponse.Data, "Id", "Definition"),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateProjectUserRoleModel projectUserRoleModel)
        {
            var mappingDto = _mapper.Map<CreateProjectUserRoleDto>(projectUserRoleModel);
            var response = await _projectUserRoleService.InsertAsync(mappingDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _projectUserRoleService.GetByIdAsync<UpdateProjectUserRoleDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound(response.Meessage);
            }
            //selectlist bilgileri beraberinde gelmeli ve model gitmeli
            var mappingModel = _mapper.Map<UpdateProjectUserRoleModel>(response.Data);
            var projectUserResponse2 = await _projectUserService.GetAllAsync();
            var projectRoleResponse2 = await _projectRoleService.GetAllAsync();
            mappingModel.ProjectUser = new SelectList(projectUserResponse2.Data, "Id", "Username");
            mappingModel.ProjectRole = new SelectList(projectRoleResponse2.Data, "Id", "Definition");
            return View(mappingModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectUserRoleModel updateProjectUserRoleModel)
        {
            var mappingDto = _mapper.Map<UpdateProjectUserRoleDto>(updateProjectUserRoleModel);
            var response = await _projectUserRoleService.UpdateAsync(mappingDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _projectUserRoleService.DeleteAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
