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
    public class SupportUserSupportConfiguration : IEntityTypeConfiguration<SupportUserSupport>
    {
        public void Configure(EntityTypeBuilder<SupportUserSupport> builder)
        {
            builder.HasIndex(x => new
            {
                x.SupportId,
                x.SupportUserId
            }).IsUnique();

            builder.HasOne(x => x.Support).WithMany(x => x.SupportUserSupports).HasForeignKey(x => x.SupportId);
            builder.HasOne(x => x.SupportUser).WithMany(x => x.SupportUserSupports).HasForeignKey(x => x.SupportUserId);
        }
    }
}
