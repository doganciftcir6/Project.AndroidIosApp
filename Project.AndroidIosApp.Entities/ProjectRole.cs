using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.Entities
{
    public class ProjectRole : BaseEntity
    {
        public string Definition { get; set; }
        //user ilişkisi (çoka-çok)
        public List<ProjectUserRole> ProjectUserRoles { get; set; }
    }
}
