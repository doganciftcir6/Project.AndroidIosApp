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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Content).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");


            builder.HasOne(x => x.ProjectUser).WithMany(x => x.Comments).HasForeignKey(x => x.ProjectUserId);
            builder.HasOne(x => x.Device).WithMany(x => x.Comments).HasForeignKey(x => x.DeviceId);
        }
    }
}
