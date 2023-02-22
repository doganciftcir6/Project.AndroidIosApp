using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.DeviceDtos
{
    public class CreateDeviceDto
    {
        public string DeviceName { get; set; }
        public int CPU { get; set; }
        public int GPU { get; set; }
        public int MEM { get; set; }
        public int UX { get; set; }
        public int TotalScore { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
        public DateTime ReleaseYear { get; set; }
    }
}
