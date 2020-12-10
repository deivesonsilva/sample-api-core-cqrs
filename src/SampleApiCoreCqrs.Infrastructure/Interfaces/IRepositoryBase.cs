using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using SampleApiCoreCqrs.Infrastructure.Entities;

namespace SampleApiCoreCqrs.Infrastructure.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, bool? asNoTracking = true);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool? asNoTracking = true);
        Task<List<TEntity>> GetAllAsync(int? skip = null, int? take = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, int? skip = null, int? take = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, int? skip = null, int? take = null);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null);
        Task CreateAsync(TEntity entity);
        Task CreateAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(Func<TEntity, bool> where);
        void Remove(IEnumerable<TEntity> entity);
    }
}
