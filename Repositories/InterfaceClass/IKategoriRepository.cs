using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceClass
{
    public interface IKategoriRepository:IRepositoryBase<Kategori>
    {
        void CreateKategori(Kategori kategori);
        void DeleteKategori(Kategori kategori);
        void UpdateKategori(Kategori kategori);
        IQueryable<Kategori> GetAllKategori(bool trackchanges);
        Kategori? GetOneKategori(int id,bool trackchanges);
    }
}
