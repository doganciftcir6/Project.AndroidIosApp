//using Microsoft.AspNetCore.Mvc;
//using Project.AndroidIosApp.Business.Abstract.Services;
//using System.Threading.Tasks;

//namespace Project.AndoridIosApp.UI.ViewComponents
//{
//    public class DeviceDetailCommentComponent : ViewComponent
//    {
//        private readonly ICommentService _commentService;

//        public DeviceDetailCommentComponent(ICommentService commentService)
//        {
//            _commentService = commentService;
//        }

//        public async Task<IViewComponentResult> InvokeAsync(int id)
//        {
//            var result = await _commentService.GetAllCommentAsyncWithUser();
//            return View(result.Data);
//        }
//    }
//}
