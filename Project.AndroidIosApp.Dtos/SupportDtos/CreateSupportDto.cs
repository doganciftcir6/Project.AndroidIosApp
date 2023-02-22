using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.SupportDtos
{
    public class CreateSupportDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int ProjectUserId { get; set; }
        public int DeviceId { get; set; }
    }
}
