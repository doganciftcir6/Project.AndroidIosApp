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
    public class SupportConfiguration : IEntityTypeConfiguration<Support>
    {
        public void Configure(EntityTypeBuilder<Support> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Content).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Sender).HasMaxLength(600).IsRequired();
            builder.Property(x => x.SenderName).HasMaxLength(600).IsRequired();
            builder.Property(x => x.Receiver).HasMaxLength(600).IsRequired();
            builder.Property(x => x.ReceiverName).HasMaxLength(600).IsRequired();
            builder.Property(x => x.Date).HasDefaultValueSql("getdate()");

            builder.HasOne(x => x.ProjectUser).WithMany(x => x.Supports).HasForeignKey(x => x.ProjectUserId);
            builder.HasOne(x => x.Device).WithMany(x => x.Supports).HasForeignKey(x => x.DeviceId);


        }
    }
}
