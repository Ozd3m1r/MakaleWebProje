using Entities.Models;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class KategoriManager : IKategoriServices
    {
        private readonly IRepositoryManager _manager;

        public KategoriManager(IRepositoryManager manager)
        {
            _manager = manager;
        }


        public IEnumerable<Kategori> GetAllKategori(bool trackChanges)
        {
            return _manager.Kategori.FindAll(trackChanges);
        }
    }
}
