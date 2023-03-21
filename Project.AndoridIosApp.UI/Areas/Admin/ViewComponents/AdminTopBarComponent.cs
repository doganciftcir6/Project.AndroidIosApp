using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminTopBarComponent : ViewComponent
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AdminTopBarComponent(IProjectUserService projectUserService, IHttpContextAccessor contextAccessor)
        {
            _projectUserService = projectUserService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_contextAccessor, _projectUserService).RunAsync();
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
