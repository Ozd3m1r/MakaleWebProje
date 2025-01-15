using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Veriler
{
    public class MakaleVeriler : IEntityTypeConfiguration<Makale>
    {
        public void Configure(EntityTypeBuilder<Makale> builder)
        {
            builder.HasKey(m => m.MakaleId);
            /*builder.HasKey(m => m.MakaleName);
            builder.HasKey(m => m.MakaleSummary);
            builder.HasKey(m => m.MakaleDate);
            builder.HasKey(m => m.MakaleIsShow);
            builder.HasKey(m => m.MakaleImagesUrl);
            builder.HasKey(m => m.KategoriId);*/
            builder.HasData(
                new Makale() { MakaleId = 1, MakaleName = "Denem1", MakaleSummary = "Makale1Özet", MakaleDate = "31.12.2013", MakaleIsShow = true, MakaleImagesUrl = "/images/1.jpg", KategoriId = 1 ,MakaleCarousel=true,MakaleContent="asd", MakaleIsShowHome=true},
                new Makale() { MakaleId = 2, MakaleName = "Denem2", MakaleSummary = "Makale2Özet", MakaleDate = "26.08.2008", MakaleIsShow = true, MakaleImagesUrl = "/images/2.jpg", KategoriId = 2 ,MakaleCarousel = true,MakaleContent="asd", MakaleIsShowHome=true },
                new Makale() { MakaleId = 3, MakaleName = "Denem3", MakaleSummary = "Makale3Özet", MakaleDate = "14.03.2021", MakaleIsShow = true, MakaleImagesUrl = "/images/3.jpg", KategoriId = 2 , MakaleCarousel = true,MakaleContent="asd", MakaleIsShowHome=true }
                );
        }
    }
}
