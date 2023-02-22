using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class ProjectUserRole : BaseEntity
    {
        public int ProjectUserId { get; set; }
        public ProjectUser ProjectUser { get; set; }
        public int ProjectRoleId { get; set; }
        public ProjectRole ProjectRole { get; set; }
    }
}
