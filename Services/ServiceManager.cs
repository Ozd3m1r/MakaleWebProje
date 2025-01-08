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



        public ServiceManager(IUsersServices user, IKategoriServices kategori, IMakaleServices makale, IMakaleDataServices makaleData)
        {
            _user = user;
            _kategori = kategori;
            _makale = makale;
            _makaleData = makaleData;
        }

        public IKategoriServices KategoriServices => _kategori;

        public IMakaleServices MakaleServices => _makale;

        public IUsersServices UsersServices => _user;
        
        public IMakaleDataServices MakaleDataServices => _makaleData;
    }
}
