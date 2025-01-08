using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public record MakaleDto
    {
        public int Id { get; set; }
        public string MakaleName { get; set; }
        public string MakaleSummary { get; set; }
        public string MakaleDate { get; set; }
        public bool MakaleIsShow { get; set; }
        public string MakaleImagesUrl { get; set; }
        public int? KategoriId { get; set; }
        public Kategori? Kategori { get; set; }
        public bool MakaleCarousel { get; set; }


    }
}
