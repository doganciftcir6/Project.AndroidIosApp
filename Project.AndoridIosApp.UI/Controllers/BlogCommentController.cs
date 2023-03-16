using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
using Project.AndroidIosApp.Dtos.CommentDtos;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Controllers
{
    public class BlogCommentController : Controller
    {
        private readonly IBlogCommentService _blogCommentService;

        public BlogCommentController(IBlogCommentService blogCommentService)
        {
            _blogCommentService = blogCommentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddBlogComment(CreateBlogCommentDto createBlogCommentDto)
        {
            createBlogCommentDto.Status = false;
            var response = await _blogCommentService.InsertBlogCommentAsync(createBlogCommentDto);

            if(response.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            else if(response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var item in response.ValidationErrors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
                return View(response.Data);
            }
            return RedirectToAction("BlogDetails", new RouteValueDictionary(
             new { controller = "Blog", action = "BlogDetails", Id = createBlogCommentDto.BlogId }));
        }
    }
}
