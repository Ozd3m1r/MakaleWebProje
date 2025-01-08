using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RepositoryManager:IRepositoryManager
    {
        private readonly RepositoryContext _context;
        private readonly IMakaleRepository _makale;
        private readonly IKategoriRepository _kategori;
        private readonly IUsersRepository? _users;
        private readonly IMakaleDataRepository _makaleData;
        public RepositoryManager(IUsersRepository? users, RepositoryContext context, IMakaleRepository makale, IKategoriRepository kategori, IMakaleDataRepository makaleData)
        {
            _context = context;
            _makale = makale;
            _kategori = kategori;
            _users = users;
            _makaleData = makaleData;
        }
        public IMakaleRepository Makale => _makale;
        IMakaleRepository IRepositoryManager.Makale => _makale;

        public IKategoriRepository Kategori =>_kategori;
        IKategoriRepository IRepositoryManager.Kategori => _kategori;

        public IUsersRepository Users => _users;
        IUsersRepository IRepositoryManager.Users => _users;

        public IMakaleDataRepository MakaleData => _makaleData;
        IMakaleDataRepository IRepositoryManager.MakaleData => _makaleData;
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
