using Project.AndroidIosApp.Dtos.SupportDtos;
using Project.AndroidIosApp.Dtos.SupportUserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.SupportUserSupportDtos
{
    public class GetSupportUserSupportDto
    {
        public int Id { get; set; }
        public int SupportUserId { get; set; }
        public GetSupportUserDto SupportUser { get; set; }
        public int SupportId { get; set; }
        public GetSupportDto Support { get; set; }
    }
}
