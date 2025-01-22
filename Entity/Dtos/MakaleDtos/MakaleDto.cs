using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.MakaleDtos
{
    public record MakaleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Makale adı zorunludur")]
        [MinLength(3, ErrorMessage = "Makale adı en az 3 karakter olmalıdır")]
        [Display(Name = "Makale Adı")]
        public string MakaleName { get; set; }

        [Required(ErrorMessage = "Makale özeti zorunludur")]
        [Display(Name = "Makale Özeti")]
        public string MakaleSummary { get; set; }

        [Display(Name = "Makale Tarihi")]
        public string? MakaleDate { get; set; } = DateTime.Now.ToString();

        [Display(Name = "Gösterilsin mi?")]
        public bool MakaleIsShow { get; set; }

        [Display(Name = "Görsel")]
        public string? MakaleImagesUrl { get; set; } = null;

        [Required(ErrorMessage = "Kategori seçimi zorunludur")]
        [Display(Name = "Kategori")]
        public int? KategoriId { get; set; }

        public Kategori? Kategori { get; set; }

        [Display(Name = "Ana Sayfada Gösterilsin mi?")]
        public bool MakaleCarousel { get; set; }
        public string MakaleContent { get; set; }
        public bool MakaleIsShowHome { get; set; }


    }
}
