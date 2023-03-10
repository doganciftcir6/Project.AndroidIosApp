using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    public class AdminSideBarComponent : ViewComponent
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdminSideBarComponent(IProjectUserService projectUserService, IHttpContextAccessor httpContextAccessor)
        {
            _projectUserService = projectUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //login olmuş kişi
            var loginUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var loginUser = await _projectUserService.FindByUserNameAsync(loginUserName);
            if(loginUser.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Success)
            {
                ViewBag.UserName = loginUser.Data.Firstname;
                ViewBag.UserLastName = loginUser.Data.Lastname;
                ViewBag.UserImage = loginUser.Data.ImageUrl;
                return View();
            }
            return View();
        }
    }
}
