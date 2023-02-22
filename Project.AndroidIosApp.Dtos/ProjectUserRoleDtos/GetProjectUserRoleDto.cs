using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.ProjectUserRoleDto
{
    public class GetProjectUserRoleDto
    {
        public int Id { get; set; }
        public int ProjectUserId { get; set; }
        public GetProjectUserDto ProjectUser { get; set; }
        public int ProjectRoleId { get; set; }
        public GetProjectRoleDto ProjectRole { get; set; }
    }
}
