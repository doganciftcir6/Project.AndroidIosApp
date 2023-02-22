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
    public class ProjectUserConfiguration : IEntityTypeConfiguration<ProjectUser>
    {
        public void Configure(EntityTypeBuilder<ProjectUser> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Firstname).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(300).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PasswordVerify).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(11).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
    
            builder.HasOne(x => x.Gender).WithMany(x => x.ProjectUsers).HasForeignKey(x => x.GenderId);
        }
    }
}
