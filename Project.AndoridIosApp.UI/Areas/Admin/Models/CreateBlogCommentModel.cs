using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class CreateBlogCommentModel
    {
        public string Content { get; set; }
        public bool Status { get; set; }


        public int ProjectUserId { get; set; }
        public SelectList ProjectUsers { get; set; }
        //Blog İlişkisi bir BlogCommentin sadece bir Blogu
        public int BlogId { get; set; }
        public SelectList Blogs { get; set; }
    }
}
