using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SampleApiCoreCqrs.Infrastructure.Entities;
using SampleApiCoreCqrs.Infrastructure.Interfaces;

namespace SampleApiCoreCqrs.Infrastructure
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity
    {
        protected readonly RepositoryContext _context;
        private IQueryable<TEntity> _query;

        private readonly DbSet<TEntity> Data;

        public RepositoryBase(RepositoryContext context)
        {
            _context = context;
            _query = _context.Set<TEntity>();

            Data = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, bool? asNoTracking = true)
        {
            return asNoTracking.Value
                ? await _query.AsNoTracking().Where(where).FirstOrDefaultAsync()
                : await _query.Where(where).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, bool? asNoTracking = true)
        {
            return asNoTracking.Value
                ? await include(_query.AsNoTracking().Where(where)).FirstOrDefaultAsync()
                : await include(_query.Where(where)).FirstOrDefaultAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(int? skip = null, int? take = null)
        {
            if (skip != null && skip.HasValue)
                _query = _query.Skip(skip.Value);

            if (take != null && take.HasValue)
                _query = _query.Take(take.Value);

            return await _query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, int? skip = null, int? take = null)
        {
            if (skip != null && skip.HasValue)
                _query = _query.Skip(skip.Value);

            if (take != null && take.HasValue)
                _query = _query.Take(take.Value);

            return await _query.Where(where).ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null)
        {
            if (skip != null && skip.HasValue)
                _query = _query.Skip(skip.Value);

            if (take != null && take.HasValue)
                _query = _query.Take(take.Value);

            _query = _query.Where(where);
            _query = orderBy(_query);

            return await _query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, int? skip = null, int? take = null)
        {
            if (skip != null && skip.HasValue)
                _query = _query.Skip(skip.Value);

            if (take != null && take.HasValue)
                _query = _query.Take(take.Value);

            _query = include(_query);
            _query = _query.Where(where);

            return await _query.ToListAsync();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, int? skip = null, int? take = null)
        {
            if (skip != null && skip.HasValue)
                _query = _query.Skip(skip.Value);

            if (take != null && take.HasValue)
                _query = _query.Take(take.Value);

            _query = include(_query);
            _query = _query.Where(where);
            _query = orderBy(_query);

            return await _query.ToListAsync();
        }

        public virtual async Task CreateAsync(TEntity entity) => await _context.Set<TEntity>().AddAsync(entity);

        public virtual async Task CreateAsync(IEnumerable<TEntity> entities) => await _context.Set<TEntity>().AddRangeAsync(entities);

        public virtual void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

        public virtual void Update(IEnumerable<TEntity> entities) => _context.Set<TEntity>().UpdateRange(entities);

        public virtual void Remove(TEntity entity) => _context.Set<TEntity>().Remove(entity);

        public virtual void Remove(Func<TEntity, bool> where) => _context.Set<TEntity>().RemoveRange(Data.ToList().Where(where));

        public virtual void Remove(IEnumerable<TEntity> entity) => _context.Set<TEntity>().RemoveRange(entity);
    }
}
