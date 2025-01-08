using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IServiceManager
    {
        IKategoriServices KategoriServices { get; }
        IMakaleServices MakaleServices { get; }
        IUsersServices UsersServices { get; }
        IMakaleDataServices MakaleDataServices { get; }
    }
}
