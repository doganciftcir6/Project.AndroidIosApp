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
    public class OSConfiguration : IEntityTypeConfiguration<OS>
    {
        public void Configure(EntityTypeBuilder<OS> builder)
        {
            builder.HasData(new OS[]
            {
                new()
                {
                    Definition = "Ios",
                    Id = 1,
                    Status = true
                },
                new()
                {
                    Definition = "Android",
                    Id = 2,
                    Status = true
                },
            });
            builder.Property(x => x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
