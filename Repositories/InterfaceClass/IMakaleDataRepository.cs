using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.InterfaceClass
{
    public interface IMakaleDataRepository:IRepositoryBase<MakaleData>
    {
        Task AddLike(int userId, int makaleId);       
        Task<int> GetLikesByMakaleId(int makaleId); 

        Task AddDislike(int userId, int makaleId);
        Task<int> GetDislikeByMakaleId(int makaleId);

        Task DeleteMakaleDataByMakaleId(int makaleId);
        // Beğeni ve beğenmeme güncelleme işlemleri
        Task UpdateLike(int userId, int makaleId, int newLikeValue);
        Task UpdateDislike(int userId, int makaleId, int newDislikeValue);

        // Makale ile ilişkili tüm MakaleData verilerini almak
        Task<IEnumerable<MakaleData>> GetMakaleDataByMakaleId(int makaleId);

        // Kullanıcıya ait tüm beğenileri/getir
        Task<IEnumerable<MakaleData>> GetMakaleDataByUserId(int userId);


    }
}
