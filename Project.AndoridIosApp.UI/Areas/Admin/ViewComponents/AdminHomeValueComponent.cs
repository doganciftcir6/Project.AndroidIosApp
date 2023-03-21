using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeValueComponent :  ViewComponent
    {
        private readonly IProjectUserService _projectUserService;
        private readonly IDeviceService _deviceService;
        private readonly IBlogService _blogService;
        private readonly ICommentService _commentService;
        private readonly IBlogCommentService _blogCommentService;

        public AdminHomeValueComponent(IProjectUserService projectUserService, IDeviceService deviceService, IBlogService blogService, ICommentService commentService, IBlogCommentService blogCommentService)
        {
            _projectUserService = projectUserService;
            _deviceService = deviceService;
            _blogService = blogService;
            _commentService = commentService;
            _blogCommentService = blogCommentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var projectUsers = await _projectUserService.GetAllAsync();
            var devices = await _deviceService.GetAllAsync();
            var blogs = await _blogService.GetAllAsync();
            var deviceComments = await _commentService.GetAllCommentAsync();
            var blogComments = await _blogCommentService.GetAllBlogCommentAsync();

            ViewBag.ProjectUsersCount = projectUsers.Data.Count;
            ViewBag.DevicesCount = devices.Data.Count;
            ViewBag.BlogsCount = blogs.Data.Count;
            ViewBag.DeviceCommentsCount = deviceComments.Data.Count;
            ViewBag.BlogCommentsCount = blogComments.Data.Count;
            return View();
        }
    }
}
