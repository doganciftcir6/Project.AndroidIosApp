using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class DeviceType  : BaseEntity
    {
        public string Definition { get; set; }
        //User bire-çok ilişkisi(bir DeviceType'ın birden çok device'si)
        public List<Device> Devices { get; set; }
    }
}
