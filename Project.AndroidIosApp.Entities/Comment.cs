using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //ProjectUser İlişkisi bir yorumun sadece bir kullanıcısı
        public int ProjectUserId { get; set; }
        public ProjectUser ProjectUser { get; set; }
        //Device İlişkisi bir yorumun sadece bir devicesi
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
