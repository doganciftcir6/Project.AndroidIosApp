using Project.AndroidIosApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.ProjectRole
{
    public class UpdateProjectRoleDto : IDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}
