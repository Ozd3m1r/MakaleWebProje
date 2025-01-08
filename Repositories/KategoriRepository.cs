using Entities.Models;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class KategoriRepository : RepositoryBase<Kategori>, IKategoriRepository
    {
        public KategoriRepository(RepositoryContext context) 
            : base(context){}

        public void CreateKategori(Kategori kategori)=>Create(kategori);

        public void DeleteKategori(Kategori kategori) => Delete(kategori);

        public void UpdateKategori(Kategori kategori) => Update(kategori);

        public IQueryable<Kategori> GetAllKategori(bool trackchanges)=>FindAll(trackchanges);

        public Kategori? GetOneKategori(int id, bool trackchanges)
        {
            return FindByCondition(k=>k.KategoriId.Equals(id),trackchanges);
        }

    }
}
