using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.InterfaceClass
{
    public interface IMakaleDataServices
    {
        // Kullanıcı bir makaleye beğeni ekler
        Task AddLike(int userId, int makaleId);

        // Kullanıcı bir makaleye beğenmeme ekler
        Task AddDislike(int userId, int makaleId);

        // Bir makale ID'sine ait toplam beğeni sayısını alır
        Task<int> GetLikesByMakaleId(int makaleId);

        // Bir makale ID'sine ait toplam beğenmeme sayısını alır
        Task<int> GetDislikeByMakaleId(int makaleId);

        // Bir makale ID'sine ait beğeni ve beğenmeme verilerini alır
        Task<IEnumerable<MakaleData>> GetMakaleDataByMakaleId(int makaleId);

        // Bir kullanıcı ID'sine ait beğeni ve beğenmeme verilerini alır
        Task<IEnumerable<MakaleData>> GetMakaleDataByUserId(int userId);

        // Beğeni sayısını günceller
        Task UpdateLike(int userId, int makaleId, int newLikeValue);

        // Beğenmeme sayısını günceller
        Task UpdateDislike(int userId, int makaleId, int newDislikeValue);

        // Bir makale silindiğinde, makale ID'sine ait verileri siler
        Task DeleteMakaleDataByMakaleId(int makaleId);
    }
}
