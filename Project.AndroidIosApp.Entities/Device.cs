using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Device : BaseEntity
    {
        public string DeviceName { get; set; }
        public int CPU { get; set; }
        public int GPU { get; set; }
        public int MEM { get; set; }
        public int UX { get; set; }
        public int TotalScore { get; set; }
        public double Price { get; set; }
        public DateTime ReleaseYear { get; set; }
        public DateTime CreateDate { get; set; }
        public string ImageUrl { get; set; }

        //Support ilişkisi(bire-çok bir devicenin birden çok supportu) 
        public List<Support> Supports { get; set; }
        //DeviceType ilişkisi(çok-bir, bir devicenin'ın bir DeviceType'ı)
        public int DeviceTypeId { get; set; }
        public DeviceType DeviceType { get; set; }
        //OS ilişkisi(çok-bir, bir devicenin'ın bir OS'ı)
        public int OSId { get; set; }
        public OS OS { get; set; }
        //Comment İlişkisi bir devicenin birden çok commenti
        public List<Comment> Comments { get; set; }
    }
}
