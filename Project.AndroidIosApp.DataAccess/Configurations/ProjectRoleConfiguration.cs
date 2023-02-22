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
    public class ProjectRoleConfiguration : IEntityTypeConfiguration<ProjectRole>
    {
        public void Configure(EntityTypeBuilder<ProjectRole> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(100).IsRequired();

            builder.HasData(new ProjectRole[]
            {
                new()
                {
                    Definition = "Admin",
                    Id = 1,
                    Status = true
                },
                new()
                {
                    Definition = "Member",
                    Id = 2,
                    Status = true
                },
                new()
                {
                    Definition = "SupportUser",
                    Id = 3,
                    Status = true
                }
            });
        }
    }
}
