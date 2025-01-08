using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MakaleDataRepository:RepositoryBase<MakaleData>, IMakaleDataRepository
    {
        public MakaleDataRepository(RepositoryContext context):base(context) { }

        // Beğeni ekleme
        public async Task AddLike(int userId, int makaleId)
        {
            var existingMakaleData = await _context.MakaleData
                .FirstOrDefaultAsync(md => md.UserId == userId && md.MakaleId == makaleId);

            if (existingMakaleData == null)
            {
                var newMakaleData = new MakaleData
                {
                    UserId = userId,
                    MakaleId = makaleId,
                    MakaleLike = 1, // İlk kez beğeni
                    MakaleDislike = 0
                };

                await _context.MakaleData.AddAsync(newMakaleData);
            }
            else
            {
                // Eğer zaten bir beğeni kaydı varsa, sadece beğeniyi artır
                existingMakaleData.MakaleLike++;
                _context.MakaleData.Update(existingMakaleData);
            }

            await _context.SaveChangesAsync();
        }

        // Beğenme sayısını almak
        public async Task<int> GetLikesByMakaleId(int makaleId)
        {
            return await _context.MakaleData
                .Where(md => md.MakaleId == makaleId)
                .SumAsync(md => md.MakaleLike);
        }

        // Beğenmeme ekleme
        public async Task AddDislike(int userId, int makaleId)
        {
            var existingMakaleData = await _context.MakaleData
                .FirstOrDefaultAsync(md => md.UserId == userId && md.MakaleId == makaleId);

            if (existingMakaleData == null)
            {
                var newMakaleData = new MakaleData
                {
                    UserId = userId,
                    MakaleId = makaleId,
                    MakaleLike = 0,
                    MakaleDislike = 1 // İlk kez beğenmeme
                };

                await _context.MakaleData.AddAsync(newMakaleData);
            }
            else
            {
                // Eğer zaten bir beğenmeme kaydı varsa, sadece beğenmeme sayısını artır
                existingMakaleData.MakaleDislike++;
                _context.MakaleData.Update(existingMakaleData);
            }

            await _context.SaveChangesAsync();
        }

        // Beğenmeme sayısını almak
        public async Task<int> GetDislikeByMakaleId(int makaleId)
        {
            return await _context.MakaleData
                .Where(md => md.MakaleId == makaleId)
                .SumAsync(md => md.MakaleDislike);
        }

        // MakaleId'ye göre tüm MakaleData verilerini sil
        public async Task DeleteMakaleDataByMakaleId(int makaleId)
        {
            var makaleDataList = await _context.MakaleData
                                                .Where(m => m.MakaleId == makaleId)
                                                .ToListAsync();

            if (makaleDataList.Any())
            {
                _context.MakaleData.RemoveRange(makaleDataList);
                await _context.SaveChangesAsync();
            }
        }

        // MakaleId'ye göre tüm MakaleData verilerini almak
        public async Task<IEnumerable<MakaleData>> GetMakaleDataByMakaleId(int makaleId)
        {
            return await _context.MakaleData
                .Where(m => m.MakaleId == makaleId)
                .ToListAsync();
        }

        // KullanıcıId'ye göre tüm MakaleData verilerini almak
        public async Task<IEnumerable<MakaleData>> GetMakaleDataByUserId(int userId)
        {
            return await _context.MakaleData
                .Where(m => m.UserId == userId)
                .ToListAsync();
        }

        // Beğeni güncelleme
        public async Task UpdateLike(int userId, int makaleId, int newLikeValue)
        {
            var existingMakaleData = await _context.MakaleData
                .FirstOrDefaultAsync(md => md.UserId == userId && md.MakaleId == makaleId);

            if (existingMakaleData != null)
            {
                existingMakaleData.MakaleLike = newLikeValue;
                _context.MakaleData.Update(existingMakaleData);
                await _context.SaveChangesAsync();
            }
        }

        // Beğenmeme güncelleme
        public async Task UpdateDislike(int userId, int makaleId, int newDislikeValue)
        {
            var existingMakaleData = await _context.MakaleData
                .FirstOrDefaultAsync(md => md.UserId == userId && md.MakaleId == makaleId);

            if (existingMakaleData != null)
            {
                existingMakaleData.MakaleDislike = newDislikeValue;
                _context.MakaleData.Update(existingMakaleData);
                await _context.SaveChangesAsync();
            }
        }
    }
}
