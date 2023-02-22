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
    public class BlogCommentConfiguration : IEntityTypeConfiguration<BlogComment>
    {
        public void Configure(EntityTypeBuilder<BlogComment> builder)
        {
            builder.Property(x => x.Content).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");


            builder.HasOne(x => x.ProjectUser).WithMany(x => x.BlogComments).HasForeignKey(x => x.ProjectUserId);
            builder.HasOne(x => x.Blog).WithMany(x => x.BlogComments).HasForeignKey(x => x.BlogId);
        }
    }
}
