using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.Interfaces;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.CommentDtos
{
    public class UpdateCommentDto : IDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateDate { get; set; }

        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUsers { get; set; }
        public int DeviceId { get; set; }
        public GetDeviceDto Devices { get; set; }
    }
}
