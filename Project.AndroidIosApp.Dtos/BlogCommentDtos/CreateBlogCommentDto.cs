using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.BlogCommentDtos
{
    public class CreateBlogCommentDto
    {
        public string Content { get; set; }
        public bool Status { get; set; }


        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUser { get; set; }
        //Blog İlişkisi bir BlogCommentin sadece bir Blogu
        public int BlogId { get; set; }
        public GetBlogDto Blog { get; set; }
    }
}
