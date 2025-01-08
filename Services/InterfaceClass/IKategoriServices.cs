using Entities.Models;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IKategoriServices
    {
        IEnumerable<Kategori> GetAllKategori(bool trackChanges);
    }
}
