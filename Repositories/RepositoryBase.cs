using Microsoft.EntityFrameworkCore;
using Repositories.InterfaceClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T>
    where T : class, new()
    {
        protected readonly RepositoryContext _context;

        protected RepositoryBase(RepositoryContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll(bool trackChanges)
        {
            if (trackChanges)
            {
                return _context.Set<T>();
            }
            else
            {
                return _context.Set<T>().AsNoTracking();
            }
        }

        public T? FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
               ? _context.Set<T>().Where(expression).SingleOrDefault()
               : _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefault();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        // Asenkron işlemler

        // Create işlemi asenkron
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // Delete işlemi asenkron
        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        // FindAll işlemi asenkron
        public async Task<IEnumerable<T>> FindAllAsync(bool trackChanges)
        {
            if (trackChanges)
            {
                return await _context.Set<T>().ToListAsync();
            }
            else
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        // FindByCondition işlemi asenkron
        public async Task<T?> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return trackChanges
                ? await _context.Set<T>().Where(expression).SingleOrDefaultAsync()
                : await _context.Set<T>().Where(expression).AsNoTracking().SingleOrDefaultAsync();
        }

        // Update işlemi asenkron
        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
