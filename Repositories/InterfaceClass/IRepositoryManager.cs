using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories.InterfaceClass
{
    public interface IRepositoryManager
    {
        IMakaleRepository Makale { get; }
        IKategoriRepository Kategori { get; }
        IUsersRepository Users { get; }
        IMakaleDataRepository MakaleData { get; }
        IUserRoleRepository UserRole { get; }
        IMakaleCommentRepository MakaleComment { get; }
        
        void Save();

    }
}
