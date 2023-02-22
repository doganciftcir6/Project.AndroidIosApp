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
    public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.HasData(new DeviceType[]
            {
                new()
                {
                    Definition = "Phone",
                    Id = 1,
                    Status = true
                },
                new()
                {
                    Definition = "Tablet",
                    Id = 2,
                    Status = true
                },
            });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
