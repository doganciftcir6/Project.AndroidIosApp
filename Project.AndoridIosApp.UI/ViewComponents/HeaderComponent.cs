using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.ViewComponents
{
    public class HeaderComponent : ViewComponent
    {
        private readonly IProjectUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public HeaderComponent(IProjectUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var loginUser = await GetLoginUser.CreateInstance(_contextAccessor, _userService).RunAsync();
            if (loginUser.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Success)
            {
                var loginUserId = loginUser.Data.Id;
                ViewBag.LoginUserId = loginUserId;
            }
            return View();
        }
    }
}
