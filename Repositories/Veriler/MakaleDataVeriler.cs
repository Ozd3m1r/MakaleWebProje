using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Veriler
{
    public class MakaleDataVeriler : IEntityTypeConfiguration<MakaleData>
    {
        public void Configure(EntityTypeBuilder<MakaleData> builder)
        {
            builder.HasKey(md=>md.MakaleDataId);
            builder.Property(md => md.MakaleLike);
            builder.Property(md=>md.MakaleDislike);
            builder.HasData(
                new MakaleData() { MakaleDataId = 1, UserId = 1, MakaleId = 1, MakaleLike = 1, MakaleDislike = 1 }
                );
        }
    }
}
