using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.SupportDtos
{
    public class GetSupportDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }

        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUser { get; set; }
        public int DeviceId { get; set; }
        public GetDeviceDto Device { get; set; }
    }
}
