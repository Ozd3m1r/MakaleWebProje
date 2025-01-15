using Entities.Models;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System.Collections.Generic;

namespace Services
{
    public class KategoriManager : IKategoriServices
    {
        private readonly IRepositoryManager _repositoryManager;

        public KategoriManager(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        // Tüm kategorileri getir
        public IEnumerable<Kategori> GetAllKategori(bool trackChanges)
        {
            return _repositoryManager.Kategori.GetAllKategori(trackChanges);
        }

        // Kategori id'ye göre kategori getir
        public Kategori GetKategoriById(int kategoriId, bool trackChanges)
        {
            return _repositoryManager.Kategori.GetOneKategori(kategoriId, trackChanges);
        }

        // Yeni kategori oluştur
        public void CreateKategori(Kategori kategori)
        {
            _repositoryManager.Kategori.CreateKategori(kategori);
            _repositoryManager.Save();
        }

        // Kategori sil
        public void DeleteKategori(Kategori kategori)
        {
            _repositoryManager.Kategori.DeleteKategori(kategori);
            _repositoryManager.Save();
        }

        // Kategori güncelle
        public void UpdateKategori(Kategori kategori)
        {
            _repositoryManager.Kategori.UpdateKategori(kategori);
            _repositoryManager.Save();
        }
    }
}
