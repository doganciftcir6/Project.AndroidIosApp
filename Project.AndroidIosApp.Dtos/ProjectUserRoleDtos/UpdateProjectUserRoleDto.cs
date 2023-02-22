using Project.AndroidIosApp.Dtos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.ProjectUserRoleDto
{
    public class UpdateProjectUserRoleDto : IDto
    {
        public int Id { get; set; }
        public int ProjectUserId { get; set; }
        public int ProjectRoleId { get; set; }
    }
}
