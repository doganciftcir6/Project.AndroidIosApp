using Project.AndroidIosApp.Core.Enums;
using Project.AndroidIosApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Project.AndroidIosApp.DataAccess.Abstract.Repositories
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity, TEntity unchanged);
        void Delete(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsyncFilter(Expression<Func<TEntity, bool>> filter );
        Task<List<TEntity>> GetAllBySorting<Tkey>(Expression<Func<TEntity, Tkey>> selector, OrderByType orderByType = OrderByType.DESC);
        Task<List<TEntity>> GetAllBySortingAndFilter<Tkey>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, Tkey>> selector, OrderByType orderByType = OrderByType.ASC);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByFilterAsync(Expression<Func<TEntity, bool>> filter , bool AsnoTracking = false);
        IQueryable<TEntity> GetQuery();

    }
}
