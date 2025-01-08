using Entities.Models;
using Entity.RequestParameters;
using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public sealed class MakaleRepository : RepositoryBase<Makale>, IMakaleRepository
    {
        public MakaleRepository(RepositoryContext context)
            : base(context) { }

        public void CreateMakale(Makale makale) => Create(makale);

        public void Deletemakale(Makale makale) => Delete(makale);

        public IQueryable<Makale> GetAllMakale(bool trackChanges) => FindAll(trackChanges);

        public IQueryable<Makale> GetAllMakaleDetails(MakaleRequestParameters m)
        {
            return m.KategoriId is null && string.IsNullOrWhiteSpace(m.Searchterm)
                ? _context
                    .Makale
                    .Include(mak => mak.Kategori)
                : _context
                    .Makale
                    .Include(mak => mak.Kategori)
                    .Where(mak =>
                        (m.KategoriId == null || mak.KategoriId.Equals(m.KategoriId)) &&
                        (string.IsNullOrWhiteSpace(m.Searchterm) || mak.MakaleName.ToLower().Contains(m.Searchterm.ToLower()))
                    );
        }

        public void OneUpdateMakale(Makale makale) => Update(makale);

        public Makale? GetOneMakale(int id, bool trackChanges)
        {
            return FindByCondition(m => m.MakaleId.Equals(id), trackChanges);
        }
        public IQueryable<Makale> GetMakaleCarousel(bool trackChanges)
        {
            return FindAll(trackChanges)
                .Where(m => m.MakaleCarousel.Equals(true));
        }

        // Asenkron İşlemler

        // GetAllMakaleAsync
        public async Task<IEnumerable<Makale>> GetAllMakaleAsync(bool trackChanges)
        {
            var query = FindAll(trackChanges);
            return await query.ToListAsync();
        }

        // GetAllMakaleDetailsAsync
        public async Task<IEnumerable<Makale>> GetAllMakaleDetailsAsync(MakaleRequestParameters m)
        {
            var query = m.KategoriId is null && string.IsNullOrWhiteSpace(m.Searchterm)
                ? _context.Makale.Include(mak => mak.Kategori)
                : _context.Makale.Include(mak => mak.Kategori)
                    .Where(mak =>
                        (m.KategoriId == null || mak.KategoriId.Equals(m.KategoriId)) &&
                        (string.IsNullOrWhiteSpace(m.Searchterm) || mak.MakaleName.ToLower().Contains(m.Searchterm.ToLower()))
                    );

            return await query.ToListAsync();
        }

        // GetMakaleCarouselAsync
        public async Task<IEnumerable<Makale>> GetMakaleCarouselAsync(bool trackChanges)
        {
            var query = FindAll(trackChanges).Where(m => m.MakaleCarousel.Equals(true));
            return await query.ToListAsync();
        }

        // GetOneMakaleAsync
        public async Task<Makale?> GetOneMakaleAsync(int id, bool trackChanges)
        {
            return await FindByConditionAsync(m => m.MakaleId.Equals(id), trackChanges);
        }
    }
}
