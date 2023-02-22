using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.DataAccess.Configurations
{
    public class ProjectUserRoleConfiguration : IEntityTypeConfiguration<ProjectUserRole>
    {
        public void Configure(EntityTypeBuilder<ProjectUserRole> builder)
        {
            builder.HasIndex(x => new
            {
                x.ProjectUserId,
                x.ProjectRoleId
            }).IsUnique();

            builder.HasOne(x => x.ProjectUser).WithMany(x => x.ProjectUserRoles).HasForeignKey(x => x.ProjectUserId);
            builder.HasOne(x => x.ProjectRole).WithMany(x => x.ProjectUserRoles).HasForeignKey(x => x.ProjectRoleId);
        }
    }
}
