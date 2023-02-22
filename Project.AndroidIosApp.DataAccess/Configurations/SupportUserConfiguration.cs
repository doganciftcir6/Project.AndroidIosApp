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
    public class SupportUserConfiguration : IEntityTypeConfiguration<SupportUser>
    {
        public void Configure(EntityTypeBuilder<SupportUser> builder)
        {
            builder.Property(x => x.SupportName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SupportLastname).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SupportEmail).HasMaxLength(200).IsRequired();
            builder.Property(x => x.SupportPhone).HasMaxLength(100).IsRequired();
            builder.Property(x => x.SupportImageUrl).IsRequired();
        }
    }
}
