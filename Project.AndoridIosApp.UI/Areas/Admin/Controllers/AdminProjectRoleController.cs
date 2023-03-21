using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Dtos.ProjectRole;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminProjectRole/{action}/{id?}")]
    [Authorize(Roles = "Admin")]
    public class AdminProjectRoleController : Controller
    {
        private readonly IProjectRoleService _projectRoleService;

        public AdminProjectRoleController(IProjectRoleService projectRoleService)
        {
            _projectRoleService = projectRoleService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _projectRoleService.GetAllAsync();
            return View(response.Data);
        }
        [HttpGet]
        public IActionResult Insert()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Insert(CreateProjectRoleDto createProjectRoleDto)
        {
            var response = await _projectRoleService.InsertAsync(createProjectRoleDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(createProjectRoleDto);
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _projectRoleService.GetByIdAsync<UpdateProjectRoleDto>(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(response.Data);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProjectRoleDto updateProjectRoleDto)
        {
            var response = await _projectRoleService.UpdateAsync(updateProjectRoleDto);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(updateProjectRoleDto);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _projectRoleService.DeleteAsync(id);
            if(response.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        } 
    }
}
