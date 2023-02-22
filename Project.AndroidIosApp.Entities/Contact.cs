using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class Contact : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Adress { get; set; }
        public string Mail { get; set; }
        public string Skype { get; set; }
        public string Phone { get; set; }
    }
}
