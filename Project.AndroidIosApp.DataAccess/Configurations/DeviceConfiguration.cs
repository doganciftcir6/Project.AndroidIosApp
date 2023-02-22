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
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(x => x.DeviceName).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.GPU).IsRequired();
            builder.Property(x => x.CPU).IsRequired();
            builder.Property(x => x.MEM).IsRequired();
            builder.Property(x => x.UX).IsRequired();
            builder.Property(x => x.TotalScore).IsRequired();
            builder.Property(x => x.ReleaseYear).IsRequired();
            builder.Property(x => x.ImageUrl).IsRequired();
            builder.Property(x => x.CreateDate).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.OS).WithMany(x => x.Devices).HasForeignKey(x => x.OSId);
            builder.HasOne(x => x.DeviceType).WithMany(x => x.Devices).HasForeignKey(x => x.DeviceTypeId);
        }
    }
}
