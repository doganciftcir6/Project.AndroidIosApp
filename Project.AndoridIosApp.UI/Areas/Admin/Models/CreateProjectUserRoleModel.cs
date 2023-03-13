using Microsoft.AspNetCore.Mvc.Rendering;
using Project.AndroidIosApp.Dtos.ProjectRole;
using Project.AndroidIosApp.Dtos.ProjectUser;

namespace Project.AndoridIosApp.UI.Areas.Admin.Models
{
    public class CreateProjectUserRoleModel
    {
        public int ProjectUserId { get; set; }
        public SelectList ProjectUser { get; set; }
        public int ProjectRoleId { get; set; }
        public SelectList ProjectRole { get; set; }
    }
}
