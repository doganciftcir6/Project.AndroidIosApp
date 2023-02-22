using Project.AndroidIosApp.Dtos.ProjectUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Dtos.ProjectUserRoleDto
{
    public class CreateProjectUserRoleDto
    {
        public int ProjectUserId { get; set; }
        public int ProjectRoleId { get; set; }
    }
}
