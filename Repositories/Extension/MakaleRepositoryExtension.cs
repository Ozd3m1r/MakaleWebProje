using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Extension
{
    public static class MakaleRepositoryExtension
    {
        // Kategoriye göre filtreleme
        public static IQueryable<Makale> FilteredByCategoryId(this IQueryable<Makale> makaleler, int? kategoriId)
        {
            if (kategoriId is null)
                // Eğer kategoriId belirtilmemişse, tüm makaleleri döndürür.
                return makaleler;
            else
                // Eğer kategoriId belirtilmişse, sadece o kategoriye ait makaleleri döndürür.
                return makaleler.Where(mak => mak.KategoriId == kategoriId);
        }

        // Arama terimine göre filtreleme
        public static IQueryable<Makale> FilteredBySearchTerm(this IQueryable<Makale> makaleler, string? searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                // Eğer arama terimi boşsa, tüm makaleleri döndürür.
                return makaleler;
            else
                // Arama terimi boş değilse, başlıkta (MakaleAd) arama yapar.
                return makaleler.Where(mak => mak.MakaleName.ToLower().Contains(searchTerm.ToLower()));
        }
    }
}
