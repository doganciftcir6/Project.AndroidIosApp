using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Project.AndoridIosApp.UI.Helpers.UserHelper;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly ICommentService _commentService;
        private readonly IProjectUserService _projectUserService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeviceController(IDeviceService deviceService, ICommentService commentService, IProjectUserService projectUserService, IHttpContextAccessor httpContextAccessor)
        {
            _deviceService = deviceService;
            _commentService = commentService;
            _projectUserService = projectUserService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _deviceService.GetAllWithOSAndDeviceTypeAsync();
            return View(result.Data);
        }
        public async Task<IActionResult> DeviceDetails(int id)
        {
            var result = await _deviceService.GetByIdWithOSDeviceTypeCommentAsync(id);
            var comment = await _commentService.GetAllCommentAsyncWithUser(id);
            var addcomment = id;
            //login olmuş kişiyi bulmak
            var loginUserResponse = GetLoginUser.CreateInstance(_httpContextAccessor, _projectUserService).RunAsync();
            if (loginUserResponse.Result.ResponseType == ResponseType.Success)
            {
                 ViewBag.UserId = loginUserResponse.Result.Data.Id;
            }
            //commentları gönderirken sadece statusu false olanları gönderelim ki admin onları true çekip öyle göstersin
            ViewBag.Comments = comment.Data.Where(x => x.Status == true);
            ViewBag.AddComment = addcomment;
            //commentların sayısını gönderirken sadece statusu false olanları gönderelim ki doğru bilgi olsun
            ViewBag.CommentCount = comment.Data.Where(x => x.Status == true).Count();

            if (result.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(result.Data);
        }
    }
}
