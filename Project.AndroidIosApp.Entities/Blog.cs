using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public DateTime CreateDate { get; set; }

        //BlogComment İlişkisi bir blogun birden çok BlogCommentı
        public List<BlogComment> BlogComments { get; set; }
    }
}
