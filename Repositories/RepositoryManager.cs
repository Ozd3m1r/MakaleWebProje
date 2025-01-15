//using Repositories.InterfaceClass;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Repositories
//{
//    public class RepositoryManager:IRepositoryManager
//    {
//        private readonly RepositoryContext _context;
//        private readonly IMakaleRepository _makale;
//        private readonly IKategoriRepository _kategori;
//        private readonly IUsersRepository? _users;
//        private readonly IMakaleDataRepository _makaleData;
//        private readonly IUserRoleRepository _userRole;
//        private readonly IMakaleCommentRepository _makaleComment;
//        public RepositoryManager(IUsersRepository? users, RepositoryContext context, IMakaleRepository makale, IKategoriRepository kategori, IMakaleDataRepository makaleData, IUserRoleRepository userRole)
//        {
//            _context = context;
//            _makale = makale;
//            _kategori = kategori;
//            _users = users;
//            _makaleData = makaleData;
//            _userRole = userRole;
//        }
//        public IMakaleRepository Makale => _makale;
//        IMakaleRepository IRepositoryManager.Makale => _makale;

//        public IKategoriRepository Kategori =>_kategori;
//        IKategoriRepository IRepositoryManager.Kategori => _kategori;

//        public IUsersRepository Users => _users;
//        IUsersRepository IRepositoryManager.Users => _users;

//        public IMakaleDataRepository MakaleData => _makaleData;
//        IMakaleDataRepository IRepositoryManager.MakaleData => _makaleData;

//        public IUserRoleRepository UsersRole => _userRole;
//        IUserRoleRepository IRepositoryManager.UserRole => _userRole;

//        public IMakaleCommentRepository makaleComment => _makaleComment;
//        IMakaleCommentRepository IRepositoryManager.MakaleComment => _makaleComment;

//        public void Save()
//        {
//            _context.SaveChanges();
//        }
//    }
//}




using Repositories.InterfaceClass;

public class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly IMakaleRepository _makale;
    private readonly IKategoriRepository _kategori;
    private readonly IUsersRepository? _users;
    private readonly IMakaleDataRepository _makaleData;
    private readonly IUserRoleRepository _userRole;
    private readonly IMakaleCommentRepository _makaleComment;

    public RepositoryManager(
        IUsersRepository? users,
        RepositoryContext context,
        IMakaleRepository makale,
        IKategoriRepository kategori,
        IMakaleDataRepository makaleData,
        IUserRoleRepository userRole,
        IMakaleCommentRepository makaleComment)  // MakaleComment eklendi
    {
        _context = context;
        _makale = makale;
        _kategori = kategori;
        _users = users;
        _makaleData = makaleData;
        _userRole = userRole;
        _makaleComment = makaleComment;  // Initialize edildi
    }

    public IMakaleRepository Makale => _makale;
    IMakaleRepository IRepositoryManager.Makale => _makale;

    public IKategoriRepository Kategori => _kategori;
    IKategoriRepository IRepositoryManager.Kategori => _kategori;

    public IUsersRepository Users => _users;
    IUsersRepository IRepositoryManager.Users => _users;

    public IMakaleDataRepository MakaleData => _makaleData;
    IMakaleDataRepository IRepositoryManager.MakaleData => _makaleData;

    public IUserRoleRepository UsersRole => _userRole;
    IUserRoleRepository IRepositoryManager.UserRole => _userRole;

    public IMakaleCommentRepository MakaleComment => _makaleComment;  // Büyük harfle başladı
    IMakaleCommentRepository IRepositoryManager.MakaleComment => _makaleComment;

    public void Save()
    {
        _context.SaveChanges();
    }
}
