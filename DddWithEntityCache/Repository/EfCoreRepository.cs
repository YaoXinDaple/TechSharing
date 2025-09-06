using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace DddWithEntityCache.Repository
{
    /// <summary>
    /// EF Core ͨ�òִ����� - ����ABP�����ƣ�ʵ��IRepository�ӿ�
    /// </summary>
    /// <typeparam name="TDbContext">DbContext����</typeparam>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <typeparam name="TKey">��������</typeparam>
    public abstract class EfCoreRepository<TDbContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly TDbContext DbContext;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly ILogger Logger;

        protected EfCoreRepository(TDbContext dbContext, ILogger logger)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            DbSet = DbContext.Set<TEntity>();
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// ��ȡʵ�������ı��ʽ��������Ҫʵ��
        /// </summary>
        protected abstract Expression<Func<TEntity, TKey>> GetKeyExpression();

        /// <summary>
        /// ����������ȡʵ���ν�ʱ��ʽ
        /// </summary>
        protected virtual Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TKey id)
        {
            var keyExpression = GetKeyExpression();
            var parameter = keyExpression.Parameters[0];
            var property = keyExpression.Body;
            var idConstant = Expression.Constant(id);
            var equalExpression = Expression.Equal(property, idConstant);
            return Expression.Lambda<Func<TEntity, bool>>(equalExpression, parameter);
        }

        /// <summary>
        /// ��ȡ�ɲ�ѯ��DbSet�����������д�����Ĭ�ϵ�Include
        /// </summary>
        protected virtual IQueryable<TEntity> GetQueryable()
        {
            return DbSet.AsQueryable();
        }

        public virtual async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var predicate = CreateEqualityExpressionForId(id);
            return await GetQueryable().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<TEntity> GetRequiredAsync(TKey id, CancellationToken cancellationToken = default)
        {
            var entity = await GetAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(TEntity).Name} with id {id} not found.");
            }
            return entity;
        }

        public virtual async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable().ToListAsync(cancellationToken);
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(predicate).ToListAsync(cancellationToken);
        }

        public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var insertedEntity = DbSet.Add(entity).Entity;
            
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }

            return insertedEntity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbContext.Attach(entity);
            var updatedEntity = DbContext.Update(entity).Entity;
            
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }

            return updatedEntity;
        }

        public virtual async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            DbSet.Remove(entity);
            
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
        }

        public virtual async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await GetRequiredAsync(id, cancellationToken);
            await DeleteAsync(entity, autoSave, cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await GetQueryable().LongCountAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().Where(predicate).LongCountAsync(cancellationToken);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await GetQueryable().AnyAsync(predicate, cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// ֧�ֻ����EF Core�ִ����࣬ʵ��ICachedRepository�ӿ�
    /// </summary>
    /// <typeparam name="TDbContext">DbContext����</typeparam>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <typeparam name="TKey">��������</typeparam>
    public abstract class CachedEfCoreRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>, ICachedRepository<TEntity, TKey>
        where TDbContext : DbContext
        where TEntity : class
    {
        protected readonly IFusionCache FusionCache;
        protected virtual string CacheKeyPrefix => $"{typeof(TEntity).Name}_";
        protected virtual TimeSpan CacheDuration => TimeSpan.FromMinutes(30);

        protected CachedEfCoreRepository(TDbContext dbContext, ILogger logger, IFusionCache fusionCache)
            : base(dbContext, logger)
        {
            FusionCache = fusionCache ?? throw new ArgumentNullException(nameof(fusionCache));
        }

        protected virtual string GetCacheKey(TKey id) => $"{CacheKeyPrefix}{id}";

        public override async Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await FusionCache.GetOrSetAsync(
                GetCacheKey(id),
                async (ctx) => await base.GetAsync(id, cancellationToken),
                options => options
                    .SetDuration(CacheDuration)
                    .SetFailSafe(true, TimeSpan.FromMinutes(5)),
                cancellationToken
            );
        }

        public virtual async Task<TEntity?> GetFromCacheAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await FusionCache.TryGetAsync<TEntity>(GetCacheKey(id));
        }

        public override async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var insertedEntity = await base.InsertAsync(entity, autoSave, cancellationToken);
            
            if (autoSave)
            {
                // ��ȡ����ʵ���ID����ӵ�����
                var keyExpression = GetKeyExpression();
                var keyFunc = keyExpression.Compile();
                var id = keyFunc(insertedEntity);
                
                await FusionCache.SetAsync(
                    GetCacheKey(id),
                    insertedEntity,
                    options => options.SetDuration(CacheDuration),
                    cancellationToken
                );
                
                Logger.LogInformation($"Added {typeof(TEntity).Name} {id} to cache after insertion");
            }

            return insertedEntity;
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var updatedEntity = await base.UpdateAsync(entity, autoSave, cancellationToken);
            
            if (autoSave)
            {
                // ��ȡ����ʵ���ID�����»���
                var keyExpression = GetKeyExpression();
                var keyFunc = keyExpression.Compile();
                var id = keyFunc(updatedEntity);
                
                await FusionCache.SetAsync(
                    GetCacheKey(id),
                    updatedEntity,
                    options => options.SetDuration(CacheDuration),
                    cancellationToken
                );
                
                Logger.LogInformation($"Updated {typeof(TEntity).Name} {id} in cache after update");
            }

            return updatedEntity;
        }

        public override async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            // ��ȡʵ��ID�����������
            var keyExpression = GetKeyExpression();
            var keyFunc = keyExpression.Compile();
            var id = keyFunc(entity);

            await base.DeleteAsync(entity, autoSave, cancellationToken);
            
            if (autoSave)
            {
                await InvalidateCacheAsync(id, cancellationToken);
            }
        }

        public override async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            await base.DeleteAsync(id, autoSave, cancellationToken);
            
            if (autoSave)
            {
                await InvalidateCacheAsync(id, cancellationToken);
            }
        }

        public virtual async Task InvalidateCacheAsync(TKey id, CancellationToken cancellationToken = default)
        {
            await FusionCache.RemoveAsync(GetCacheKey(id));
            Logger.LogInformation($"Removed {typeof(TEntity).Name} {id} from cache");
        }

        public virtual async Task InvalidateCacheAsync(params TKey[] ids)
        {
            foreach (var id in ids)
            {
                await FusionCache.RemoveAsync(GetCacheKey(id));
            }
            Logger.LogInformation($"Invalidated cache for {ids.Length} {typeof(TEntity).Name} entities");
        }

        public virtual ValueTask InvalidateAllCacheAsync(CancellationToken cancellationToken = default)
        {
            return FusionCache.RemoveByTagAsync(CacheKeyPrefix);
        }
    }
}