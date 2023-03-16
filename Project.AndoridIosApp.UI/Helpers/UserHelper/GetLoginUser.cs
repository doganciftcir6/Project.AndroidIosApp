using Microsoft.AspNetCore.Http;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Core.Utilities.Results.Interface;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Helpers.UserHelper
{
    public class GetLoginUser
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IProjectUserService _projectUserService;

        private static GetLoginUser _instance;
        public GetLoginUser(IHttpContextAccessor httpContextAccessor, IProjectUserService projectUserService)
        {
            _httpContextAccessor = httpContextAccessor;
            _projectUserService = projectUserService;
        }
        //bu class dependecny kullandığı için newlenemeyeceği için bu kullanımı yapıp newlemekten kurtulalım.
        public static GetLoginUser CreateInstance(IHttpContextAccessor httpContextAccessor, IProjectUserService projectUserService)
        {
            _instance = new GetLoginUser(httpContextAccessor, projectUserService);
            return _instance;
        }
        public async Task<IDataResponse<GetProjectUserDto>> RunAsync()
        {
            //login olmuş kişiyi bulmak
            var loginUserName = _httpContextAccessor.HttpContext.User.Identity.Name;
            var loginUser = await _projectUserService.FindByUserNameAsync(loginUserName);
            if (loginUser.ResponseType == AndroidIosApp.Core.Enums.ResponseType.NotFound)
            {
                return new DataResponse<GetProjectUserDto>(ResponseType.NotFound, "Login olmuş kullanıcı bulunamadı.");
            }
            return new DataResponse<GetProjectUserDto>(ResponseType.Success, loginUser.Data);
        }
    }
}
