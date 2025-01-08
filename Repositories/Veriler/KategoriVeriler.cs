using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Veriler
{
    public class KategoriVeriler:IEntityTypeConfiguration<Kategori>
    {
        public void Configure(EntityTypeBuilder<Kategori> builder)
        {
            builder.HasKey(k=>k.KategoriId);
            builder.Property(k=>k.KategoriName);
            builder.HasData(
                new Kategori() { KategoriId =1, KategoriName ="Bilim Kurgu"},
                new Kategori() { KategoriId =2, KategoriName ="Yazılım"}

                );
        }
    }
}
