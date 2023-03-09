using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
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
            var loginUserName = _contextAccessor.HttpContext.User.Identity.Name;
            var loginUser = await _projectUserService.FindByUserNameAsync(loginUserName);
            if(loginUser.ResponseType == AndroidIosApp.Core.Enums.ResponseType.Success)
            {
               return View(loginUser.Data);
            }
            return View();
        }
    }
}
