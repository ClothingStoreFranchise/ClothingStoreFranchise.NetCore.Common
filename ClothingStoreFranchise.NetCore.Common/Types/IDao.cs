using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStoreFranchise.NetCore.Common.Types
{
    public interface IDao<TEntity> where TEntity : class
    {
        /// <summary>
        /// Create entry in the database
        /// </summary>
        /// <param name="entity">Entity to persist</param>
        /// <returns></returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Creates the list of entities in the database
        /// </summary>
        /// <param name="entities">entities to persist</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> CreateAsync(ICollection<TEntity> entities);

        /// <summary>
        /// Update entry in the database
        /// </summary>
        /// <param name="entity">Entity to update</param>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Updates the list of entities in the database
        /// </summary>
        /// <param name="entities">entities to update</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> UpdateAsync(ICollection<TEntity> entities);

        /// <summary>
        /// Delete entity in the Database
        /// </summary>
        /// <param name="entity">entity to delete</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete list of entities
        /// </summary>
        /// <param name="listEntities">entities to delete</param>
        Task DeleteAsync(ICollection<TEntity> listEntities);

        /// <summary>
        /// Load all entities
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> LoadAllAsync();

        /// <summary>
        /// Load all entities which fullfil a given predicate
        /// </summary>
        /// <param name="predicate">query condition</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> FindWhereAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> FindSingleWhereAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns if there is any entity
        /// </summary>
        /// <returns></returns>
        Task<bool> AnyAsync();

        /// <summary>
        /// Returns if there is any entity that satisfies the condition of the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns total number of entities
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();

        /// <summary>
        /// Returns number of entites that satisfy the condition of the predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
    }

    public interface IDao<TEntity, TAppId> : IDao<TEntity> where TEntity : Entity<TAppId>
    {
        /// <summary>
        /// Delete entity by AppId
        /// </summary>
        /// <param name="appId">Entity AppId</param>
        Task DeleteAsync(TAppId appId);

        /// <summary>
        /// Delete list of entities by AppId
        /// </summary>
        /// <param name="listAppId">list of app Ids of entities to be deleted</param>
        Task DeleteAsync(ICollection<TAppId> listAppId);

        /// <summary>
        /// Delete entity by its synthetic key
        /// </summary>
        /// <param name="id">id of entity to delete</param>
        Task DeleteByIdAsync(long id);

        /// <summary>
        /// Delete list of entities by its synthetic key
        /// </summary>
        /// <param name="listAppId">list of synthetic keys of entities to be deleted</param>
        Task DeleteByIdAsync(ICollection<long> listId);

        /// <summary>
        /// Load by AppId. Returns null if it does not exist
        /// </summary>
        /// <param name="appId">Entity AppId</param>
        /// <returns></returns>
        Task<TEntity> LoadAsync(TAppId appId);

        /// <summary>
        /// Load multiple entities by AppId
        /// </summary>
        /// <param name="listAppId">list of app Ids of entities to be loaded</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> LoadAsync(ICollection<TAppId> listAppId);

        TEntity Load(TAppId appId);

        /// <summary>
        /// Load by id. Returns null if it does not exist
        /// </summary>
        /// <param name="id">synthetic key of the entity to load</param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(long id);

        /// <summary>
        /// Load entity without any related entity by its synthetic key
        /// </summary>
        /// <param name="id">synthetic key of the entity to load</param>
        /// <returns></returns>
        Task<TEntity> LoadOnlyPropertiesByIdAsync(long id);

        /// <summary>
        /// Load all entities without any related entity by synthetic keys
        /// </summary>
        /// <param name="ids">list of synthetic keys of entities to be loaded</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> LoadOnlyPropertiesByIdAsync(IEnumerable<long> ids);

        /// <summary>
        /// Loads by AppId without any related entity
        /// </summary>
        /// <param name="appId">app Id of entity to be loaded</param>
        /// <returns></returns>
        Task<TEntity> LoadOnlyPropertiesAsync(TAppId appId);

        /// <summary>
        /// Loads entities by appIds without any related entity
        /// </summary>
        /// <param name="appIds">list of app Ids of entities to be loaded</param>
        /// <returns></returns>
        Task<ICollection<TEntity>> LoadOnlyPropertiesAsync(IEnumerable<TAppId> appIds);
    }
}
