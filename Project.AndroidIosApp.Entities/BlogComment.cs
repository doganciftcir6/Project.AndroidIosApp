using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class BlogComment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //ProjectUser İlişkisi bir BlogCommentin sadece bir ProjectUseri
        public int ProjectUserId { get; set; }
        public ProjectUser ProjectUser { get; set; }
        //Blog İlişkisi bir BlogCommentin sadece bir Blogu
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
