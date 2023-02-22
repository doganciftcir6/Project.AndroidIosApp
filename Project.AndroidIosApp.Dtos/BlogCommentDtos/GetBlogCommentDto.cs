using Project.AndroidIosApp.Dtos.BlogDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.BlogCommentDtos
{
    public class GetBlogCommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Status { get; set; }


        //ProjectUser İlişkisi bir BlogCommentin sadece bir ProjectUseri
        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUser { get; set; }
        //Blog İlişkisi bir BlogCommentin sadece bir Blogu
        public int BlogId { get; set; }
        public GetBlogDto Blog { get; set; }
    }
}
