using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Gender : BaseEntity
    {
        public string Definition { get; set; }
        //User bire-çok ilişkisi(bir gender'ın birden çok user'ı)
        public List<ProjectUser> ProjectUsers { get; set; }
    }
}
