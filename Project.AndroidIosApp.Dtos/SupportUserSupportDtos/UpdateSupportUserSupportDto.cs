using Project.AndroidIosApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.SupportUserSupportDtos
{
    public class UpdateSupportUserSupportDto : IDto
    {
        public int Id { get; set; }
        public int SupportUserId { get; set; }
        public int SupportId { get; set; }
    }
}
