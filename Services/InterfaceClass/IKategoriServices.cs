using Entities.Models;
using System.Collections.Generic;

namespace Services.InterfaceClass
{
    public interface IKategoriServices
    {
        // Tüm kategorileri getir
        IEnumerable<Kategori> GetAllKategori(bool trackChanges);

        // Bir kategori getir
        Kategori GetKategoriById(int kategoriId, bool trackChanges);

        // Kategori oluştur
        void CreateKategori(Kategori kategori);

        // Kategori sil
        void DeleteKategori(Kategori kategori);

        // Kategori güncelle
        void UpdateKategori(Kategori kategori);
    }
}
