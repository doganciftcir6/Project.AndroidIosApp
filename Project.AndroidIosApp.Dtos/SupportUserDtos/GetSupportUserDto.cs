using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.SupportUserDtos
{
    public class GetSupportUserDto
    {
        public int Id { get; set; }
        public string SupportName { get; set; }
        public string SupportLastname { get; set; }
        public string SupportEmail { get; set; }
        public string SupportPhone { get; set; }
        public string SupportImageUrl { get; set; }
    }
}
