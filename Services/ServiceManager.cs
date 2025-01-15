using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManager:IServiceManager
    {
        readonly IMakaleServices _makale;
        readonly IKategoriServices _kategori;
        readonly IUsersServices _user;
        readonly IMakaleDataServices _makaleData;
        readonly IUserRoleServices _userRole;
        readonly IMakaleCommentServices _makaleComment;

        public ServiceManager(IUsersServices user, IKategoriServices kategori, IMakaleServices makale, IMakaleDataServices makaleData, IUserRoleServices userRole, IMakaleCommentServices makaleComment)
        {
            _user = user;
            _kategori = kategori;
            _makale = makale;
            _makaleData = makaleData;
            _userRole = userRole;
            _makaleComment = makaleComment;
        }

        public IKategoriServices KategoriServices => _kategori;

        public IMakaleServices MakaleServices => _makale;

        public IUsersServices UsersServices => _user;
        
        public IMakaleDataServices MakaleDataServices => _makaleData;

        public IUserRoleServices UserRoleServices => _userRole;
        public IMakaleCommentServices MakaleCommentServices => _makaleComment;
    }
}
