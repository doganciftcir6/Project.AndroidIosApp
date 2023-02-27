using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Core.Utilities.Results.Concrete;
using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
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
                return View(response.Data);
            }
            return RedirectToAction("DeviceDetails", new RouteValueDictionary(
              new { controller = "Device", action = "DeviceDetails", Id = createCommentDto.DeviceId }));
        }
    }
}
