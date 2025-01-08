using Microsoft.AspNetCore.Mvc.Rendering;

namespace MakaleWebProje.Areas.Admin.Model
{
  
        public class MakaleCreateViewModel
        {
            public string MakaleName { get; set; }
            public string MakaleSummary { get; set; }
            public string MakaleDate { get; set; }
            public bool MakaleIsShow { get; set; }
            public bool MakaleCarousel { get; set; }
            public int KategoriId { get; set; } // Kategori seçilecek
            public string MakaleImagesUrl { get; set; }

            public IEnumerable<SelectListItem> Categories { get; set; } // Kategoriler listesi
        }

    
}
