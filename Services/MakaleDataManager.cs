using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Entity.RequestParameters;
using Repositories.InterfaceClass;
using Services.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MakaledataManager:IMakaleDataServices
    {
        private readonly IRepositoryManager _manager;
        private readonly IMapper _mapper;

        public MakaledataManager( IMapper mapper, IRepositoryManager manager)
        {
            
            _mapper = mapper;
            _manager = manager;
        }
        // Kullanıcı bir makaleye beğeni ekler
        public async Task AddLike(int userId, int makaleId)
        {
            await _manager.MakaleData.AddLike(userId, makaleId);
        }

        // Kullanıcı bir makaleye beğenmeme ekler
        public async Task AddDislike(int userId, int makaleId)
        {
            await _manager.MakaleData.AddDislike(userId, makaleId);
        }

        // Bir makale ID'sine ait toplam beğeni sayısını alır
        public async Task<int> GetLikesByMakaleId(int makaleId)
        {
            return await _manager.MakaleData.GetLikesByMakaleId(makaleId);
        }

        // Bir makale ID'sine ait toplam beğenmeme sayısını alır
        public async Task<int> GetDislikeByMakaleId(int makaleId)
        {
            return await _manager.MakaleData.GetDislikeByMakaleId(makaleId);
        }

        // Bir makale ID'sine ait beğeni ve beğenmeme verilerini alır
        public async Task<IEnumerable<MakaleData>> GetMakaleDataByMakaleId(int makaleId)
        {
            return await _manager.MakaleData.GetMakaleDataByMakaleId(makaleId);
        }

        // Bir kullanıcı ID'sine ait beğeni ve beğenmeme verilerini alır
        public async Task<IEnumerable<MakaleData>> GetMakaleDataByUserId(int userId)
        {
            return await _manager.MakaleData.GetMakaleDataByUserId(userId);
        }

        // Beğeni sayısını günceller
        public async Task UpdateLike(int userId, int makaleId, int newLikeValue)
        {
            await _manager.MakaleData.UpdateLike(userId, makaleId, newLikeValue);
        }

        // Beğenmeme sayısını günceller
        public async Task UpdateDislike(int userId, int makaleId, int newDislikeValue)
        {
            await _manager.MakaleData.UpdateDislike(userId, makaleId, newDislikeValue);
        }

        // Bir makale silindiğinde, makale ID'sine ait verileri siler
        public async Task DeleteMakaleDataByMakaleId(int makaleId)
        {
            await _manager.MakaleData.DeleteMakaleDataByMakaleId(makaleId);
        }
    }
}
