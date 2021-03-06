﻿using ClothingStoreFranchise.NetCore.Common.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Common.EntityFramework
{
    public abstract class AbstractDao<TEntity, TDbContext> : IDao<TEntity> 
        where TEntity : class 
        where TDbContext : DbContext
    {
        protected DbContext Context { get; set; }

#pragma warning disable S3442 // "abstract" classes should not have "public" constructors // this class should not be inherited explicitly outside this project
        internal AbstractDao(TDbContext contextContainer)
#pragma warning restore S3442 // "abstract" classes should not have "public" constructors
        {
            Context = contextContainer;
            
        }

        public async virtual Task<TEntity> CreateAsync(TEntity entity)
        {
            Context.Add(entity);
            await SaveChangesAsync();
            return entity;
        }

        public async virtual Task<ICollection<TEntity>> CreateAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Context.Add(entity);
            }
            await SaveChangesAsync();
            return entities;
        }

        public abstract Task<TEntity> UpdateAsync(TEntity entity);

        public abstract Task<ICollection<TEntity>> UpdateAsync(ICollection<TEntity> entities);

        public async virtual Task DeleteAsync(TEntity entity)
        {
            Context.Remove(entity);
            await SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(ICollection<TEntity> listEntities)
        {
            Context.RemoveRange(listEntities);
            await SaveChangesAsync();
        }

        public async virtual Task<ICollection<TEntity>> LoadAllAsync()
        {
            return await QueryTemplate().ToListAsync();
        }

        public virtual async Task<ICollection<TEntity>> FindWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await QueryTemplate().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> FindSingleWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await QueryTemplate().Where(predicate).SingleOrDefaultAsync();
        }

        public async virtual Task<bool> AnyAsync()
        {
            return await QueryTemplate().AnyAsync();
        }

        public async virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await QueryTemplate().AnyAsync(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await QueryTemplate().CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await QueryTemplate().CountAsync(predicate);
        }

        protected virtual IQueryable<TEntity> QueryTemplate()
        {
            return Context.Set<TEntity>();
        }

        internal async Task<bool> SaveChangesAsync()
        {
            try
            {
                return await Context.SaveChangesAsync() == 1;
            }
            catch (Exception ex)
            {
                //_logger.LogDebug(GetHashCode(), ex, "DbContext Validation Errors!");
                return false;
            }
        }
    }
}
