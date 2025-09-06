using System.Linq.Expressions;

namespace DddWithEntityCache.Repository
{
    /// <summary>
    /// 通用仓储接口 - 定义基础CRUD操作
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 根据主键获取实体（可能为空）
        /// </summary>
        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键获取实体（不存在时抛出异常）
        /// </summary>
        Task<TEntity> GetRequiredAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查找第一个实体
        /// </summary>
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 插入实体
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 更新实体
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除实体
        /// </summary>
        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取实体总数
        /// </summary>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件获取实体总数
        /// </summary>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 检查是否存在满足条件的实体
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存更改
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 只读仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 根据主键获取实体（可能为空）
        /// </summary>
        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键获取实体（不存在时抛出异常）
        /// </summary>
        Task<TEntity> GetRequiredAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取所有实体列表
        /// </summary>
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件获取实体列表
        /// </summary>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件查找第一个实体
        /// </summary>
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取实体总数
        /// </summary>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据条件获取实体总数
        /// </summary>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// 检查是否存在满足条件的实体
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// 支持缓存的仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface ICachedRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 从缓存中获取实体
        /// </summary>
        Task<TEntity?> GetFromCacheAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 使单个实体缓存失效
        /// </summary>
        Task InvalidateCacheAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// 使多个实体缓存失效
        /// </summary>
        Task InvalidateCacheAsync(params TKey[] ids);

        /// <summary>
        /// 使所有实体缓存失效
        /// </summary>
        ValueTask InvalidateAllCacheAsync(CancellationToken cancellationToken = default);
    }
}