using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.AndroidIosApp.Business.Abstract.Services;
using System.Data;
using System.Threading.Tasks;

namespace Project.AndoridIosApp.UI.Areas.Admin.ViewComponents
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeRecentlyAddedBlogComponent : ViewComponent
    {
        private readonly IBlogService _blogService;

        public AdminHomeRecentlyAddedBlogComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _blogService.GetAllBySortingToCreateDateAsync();
            return View(blogs.Data);
        }
    }
}
