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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Subtitle).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Company).IsRequired();
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");
        }
    }
}
