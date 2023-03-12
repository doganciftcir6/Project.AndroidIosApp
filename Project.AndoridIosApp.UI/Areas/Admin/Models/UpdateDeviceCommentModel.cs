using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class UpdateDeviceCommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateDate { get; set; }


        public int ProjectUserId { get; set; }
        public SelectList ProjectUsers { get; set; }
        public int DeviceId { get; set; }
        public SelectList Devices { get; set; }
    }
}
