﻿using ClothingStoreFranchise.NetCore.Common.Exceptions;
using ClothingStoreFranchise.NetCore.Common.Extensible;
using ClothingStoreFranchise.NetCore.Common.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Common.EntityFramework
{
    /// <summary>
    /// Only use this class for DAOs whose entities are non-instantiable . Otherwise use <see cref="BaseDaoEfImpl{TEntity, TAppId}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TAppId"></typeparam>
    public abstract class BaseAbstractEntitiesDao<TEntity, TAppId, TDbContext> : AbstractDao<TEntity, TDbContext>, IDao<TEntity, TAppId>
       where TEntity : ExtensibleEntity<TAppId>, IExtensibleEntity
       where TDbContext : DbContext
    {
        protected BaseAbstractEntitiesDao(TDbContext contextContainer) : base(contextContainer)
        {
        }

        public async virtual Task DeleteAsync(TAppId appId)
        {
            TEntity entity = await LoadAsync(appId);

            if (entity == null)
            {
                throw new EntityDoesNotExistException("Trying to delete a non existing entity");
            }

            await DeleteAsync(entity);
        }

        public async virtual Task DeleteAsync(ICollection<TAppId> listAppId)
        {
            var entity = await LoadAsync(listAppId);
            if (entity == null)
            {
                throw new EntityDoesNotExistException("Trying to delete a non existing entity");
            }
            await DeleteAsync(entity);
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            BeforeUpdateEntity(entity);

            await SaveChangesAsync();
            return entity;
        }

        public override async Task<ICollection<TEntity>> UpdateAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                BeforeUpdateEntity(entity);
            }

            await SaveChangesAsync();
            return entities;
        }

        private void BeforeUpdateEntity(TEntity entity)
        {
            var local = Context.Set<TEntity>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(1));

            // check if local is not null 
            if (local != null)
            {
                // detach
                Context.Entry(local).State = EntityState.Detached;
            }
            // set Modified flag in your entry
            Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity Load(TAppId appId)
        {
            return QueryTemplate()
                .SingleOrDefault(AppIdCondition(appId));
        }

        public async virtual Task<TEntity> LoadAsync(TAppId appId)
        {
            var entity = await QueryTemplate()
                .SingleOrDefaultAsync(AppIdCondition(appId));

            if (entity == null)
            {
                throw new EntityDoesNotExistException();
            }
            return entity;
        }

        public async virtual Task<ICollection<TEntity>> LoadAsync(ICollection<TAppId> listAppId)
        {
            return await FindWhereAsync(AppIdConditionForCollections(listAppId));
        }

        public async virtual Task<TEntity> LoadOnlyPropertiesAsync(TAppId appId)
        {
            //QueryTrackingBehavior trackingBehavior = Context.SetNoTrackingBehavior();

            TEntity entity = await QueryTemplate().Where(AppIdCondition(appId)).SingleOrDefaultAsync();

            //Context.ChangeTracker.QueryTrackingBehavior = trackingBehavior;

            return entity;
        }

        public async virtual Task<ICollection<TEntity>> LoadOnlyPropertiesAsync(IEnumerable<TAppId> appIds)
        {
            ICollection<TEntity> entities = await QueryTemplate().Where(AppIdConditionForCollections(appIds.ToList())).ToListAsync();

            return entities;
        }

        public async virtual Task<TEntity> FindByIdAsync(long id)
        {
            TEntity result = await QueryTemplate()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (result == null)
            {
                //throw new EntityDoesNotExistException();
            }
            return result;
        }

        protected virtual Expression<Func<TEntity, bool>> AppIdCondition(TAppId appId)
        {
            return t => t.Id.Equals(appId);
        }

        protected virtual Expression<Func<TEntity, bool>> AppIdConditionForCollections(ICollection<TAppId> collectionAppIds)
        {
            return e => ((ICollection<string>)collectionAppIds).Contains(((IAutoGeneratedAppId)e).AutoGeneratedAppId);
        }

        public async virtual Task DeleteByIdAsync(long id)
        {
            var entity = await FindByIdAsync(id);
            await DeleteAsync(entity);
        }

        public async virtual Task DeleteByIdAsync(ICollection<long> listId)
        {
            var entities = await FindWhereAsync(t => listId.Contains(t.Id));
            //await Task.WhenAll(entities);
            await DeleteAsync(entities);
        }

        public async Task<TEntity> LoadOnlyPropertiesByIdAsync(long id)
        {
            //QueryTrackingBehavior trackingBehavior = Context.SetNoTrackingBehavior();

            TEntity entity = await Context.Set<TEntity>().FindAsync(id);

            //Context.ChangeTracker.QueryTrackingBehavior = trackingBehavior;

            return entity;
        }

        public async Task<ICollection<TEntity>> LoadOnlyPropertiesByIdAsync(IEnumerable<long> ids)
        {
            //QueryTrackingBehavior trackingBehavior = Context.SetNoTrackingBehavior();

            ICollection<TEntity> entities = await QueryTemplate().Where(entity => ids.Contains(entity.Id)).ToListAsync();

            //Context.ChangeTracker.QueryTrackingBehavior = trackingBehavior;

            return entities;
        }
    }
}
