using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class DeviceController : Controller
    {
        private readonly IDeviceService _deviceService;
        private readonly ICommentService _commentService;

        public DeviceController(IDeviceService deviceService, ICommentService commentService)
        {
            _deviceService = deviceService;
            _commentService = commentService;
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

            ViewBag.Comments = comment.Data;
            ViewBag.AddComment = addcomment;
            ViewBag.CommentCount = comment.Data.Count();

            if (result.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(result.Data);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
        {
            createCommentDto.Status = true;
            var response = await _commentService.InsertCommentAsync(createCommentDto);

            if (response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            else if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                //return response.Data;
            }
            return RedirectToAction("DeviceDetails", new RouteValueDictionary(
              new { controller = "Device", action = "DeviceDetails", Id = createCommentDto.DeviceId }));
        }
    }
}
