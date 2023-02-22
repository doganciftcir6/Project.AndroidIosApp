using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Support : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        //user ilişkisi(çok-bir bir supportun yalnızca bir user'ı)
        public int ProjectUserId { get; set; }
        public ProjectUser ProjectUser { get; set; }
        //SupportUser ilişkisi(çoka-çok bir supportun birden çok usersapportu)
        public List<SupportUserSupport> SupportUserSupports { get; set; }
        //Device ilişkisi(çok-bir bir supportun bir bir devicesi) 
        public int DeviceId { get; set; }
        public Device Device { get; set; }
    }
}
