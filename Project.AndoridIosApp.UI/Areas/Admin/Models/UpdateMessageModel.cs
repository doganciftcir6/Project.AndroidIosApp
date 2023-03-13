using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndroidIosApp.Dtos.DeviceDtos;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class UpdateMessageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public bool Status { get; set; }

        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUser { get; set; }
        public int DeviceId { get; set; }
        public SelectList Device { get; set; }
    }
}
