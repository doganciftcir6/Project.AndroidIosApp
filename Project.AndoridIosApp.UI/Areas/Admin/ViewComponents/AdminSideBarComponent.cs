using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
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
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_httpContextAccessor, _projectUserService).RunAsync();
            if (loginUserResponse.Result.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Success)
            {
                ViewBag.UserName = loginUserResponse.Result.Data.Firstname;
                ViewBag.UserLastName = loginUserResponse.Result.Data.Lastname;
                ViewBag.UserImage = loginUserResponse.Result.Data.ImageUrl;
                return View();
            }
            return View();
        }
    }
}
