using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Dtos.BlogCommentDtos;
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
        public async Task<IActionResult> BlogDetails(int id)
        {
            var result = await _blogCommentService.GetByIdWithBlogAndUserTableAsync(id);
            if (result.ResponseType == ResponseType.NotFound)
            {
                return NotFound();
            }
            return View(result.Data);
        }
    }
}
