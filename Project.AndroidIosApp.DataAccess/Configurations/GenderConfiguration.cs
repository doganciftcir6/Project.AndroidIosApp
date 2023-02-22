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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(new Gender[]
            {
                new()
                {
                    Definition = "Erkek",
                    Id = 1,
                    Status = true
                },
                new()
                {
                    Definition = "Kadın",
                    Id = 2,
                    Status = true
                },
                 new()
                {
                    Definition = "Belirtmek İstemiyorum",
                    Id = 3,
                    Status = true
                }
            });
            builder.Property(x=> x.Definition).IsRequired().HasMaxLength(100);
        }
    }
}
