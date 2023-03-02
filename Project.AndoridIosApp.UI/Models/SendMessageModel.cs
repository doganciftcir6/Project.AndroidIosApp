using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace Project.AndoridIosApp.UI.Models
{
    public class SendMessageModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public DateTime Date { get; set; }
        public int ProjectUserId { get; set; }
        public SelectList ProjectUsers { get; set; }
        public int DeviceId { get; set; }
        public SelectList Devices { get; set; }
    }
}
