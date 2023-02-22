using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class OS : BaseEntity
    {
        public string Definition { get; set; }
        //User bire-çok ilişkisi(bir OS'ın birden çok device'si)
        public List<Device> Devices { get; set; }
    }
}
