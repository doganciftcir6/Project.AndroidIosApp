using Microsoft.EntityFrameworkCore;
using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.DataAccess.Abstract.Repositories;
using Project.AndroidIosApp.DataAccess.Contexts.EntityFramework;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.DataAccess.Concrete.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AndroidIosContext _context;

        public Repository(AndroidIosContext context)
        {
            _context = context;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsyncFilter(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<TEntity>> GetAllBySorting<Tkey>(Expression<Func<TEntity, Tkey>> selector, OrderByType orderByType = OrderByType.ASC)
        {
             return orderByType == OrderByType.ASC ? await _context.Set<TEntity>().AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<TEntity>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllBySortingAndFilter<Tkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, Tkey>> selector, OrderByType orderByType = OrderByType.ASC)
        {
            return orderByType == OrderByType.ASC ? await _context.Set<TEntity>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() : await _context.Set<TEntity>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter, bool AsnoTracking = false)
        {
            return !AsnoTracking ? await _context.Set<TEntity>().SingleOrDefaultAsync(filter) : await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _context.Set<TEntity>().AsQueryable();
        }
        public async Task InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity, TEntity unchanged)
        {
            _context.Entry(unchanged).CurrentValues.SetValues(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
