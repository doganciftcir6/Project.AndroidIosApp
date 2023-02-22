using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class SupportUserSupport : BaseEntity
    {
        public int SupportUserId { get; set; }
        public SupportUser SupportUser { get; set; }
        public int SupportId { get; set; }
        public Support Support { get; set; }
    }
}
