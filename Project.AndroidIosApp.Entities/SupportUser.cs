using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class SupportUser : BaseEntity
    {
        public string SupportName { get; set; }
        public string SupportLastname { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string SupportImageUrl { get; set; }
        //SupportUser ilişkisi(çoka-çok bir supportuserınn birden çok supportu)
        public List<SupportUserSupport> SupportUserSupports { get; set; }
    }
}
