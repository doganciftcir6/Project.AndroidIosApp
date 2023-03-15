using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndroidIosApp.Dtos.DeviceTypeDtos;
using Project.AndroidIosApp.Dtos.OSDtos;
using System;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class UpdateDeviceModel
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
        public string ReleaseYear { get; set; }

        public int OSId { get; set; }
        public SelectList OS { get; set; }
        public int DeviceTypeId { get; set; }
        public SelectList DeviceType { get; set; }
    }
}
