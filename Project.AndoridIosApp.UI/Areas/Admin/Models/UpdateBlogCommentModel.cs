using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class UpdateBlogCommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }


        public int ProjectUserId { get; set; }
        public SelectList ProjectUsers { get; set; }
        //Blog İlişkisi bir BlogCommentin sadece bir Blogu
        public int BlogId { get; set; }
        public SelectList Blogs { get; set; }
    }
}
