using Project.AndroidIosApp.Dtos.CommentDtos;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Dtos.GenderDto;
using Project.AndroidIosApp.Dtos.OSDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.DeviceDtos
{
    public class GetDeviceDto
    {
        public int Id { get; set; }
        public string DeviceName { get; set; }
        public int CPU { get; set; }
        public int GPU { get; set; }
        public int MEM { get; set; }
        public int UX { get; set; }
        public int TotalScore { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public DateTime ReleaseYear { get; set; }
        public DateTime CreateDate { get; set; }

        public int OSId { get; set; }
        public GetOSDto OS { get; set; }
        public int DeviceTypeId { get; set; }
        public GetDeviceTypeDto DeviceType { get; set; }

        public List<GetCommentDto> Comments { get; set; }


    }
}
