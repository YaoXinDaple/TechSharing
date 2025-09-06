using System.Linq.Expressions;

namespace DddWithEntityCache.Repository
{
    /// <summary>
    /// ͨ�òִ��ӿ� - �������CRUD����
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <typeparam name="TKey">��������</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// ����������ȡʵ�壨����Ϊ�գ�
        /// </summary>
        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ�壨������ʱ�׳��쳣��
        /// </summary>
        Task<TEntity> GetRequiredAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ��ȡ����ʵ���б�
        /// </summary>
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ���б�
        /// </summary>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// �����������ҵ�һ��ʵ��
        /// </summary>
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����ʵ��
        /// </summary>
        Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����ʵ��
        /// </summary>
        Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// ɾ��ʵ��
        /// </summary>
        Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// ��������ɾ��ʵ��
        /// </summary>
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);

        /// <summary>
        /// ��ȡʵ������
        /// </summary>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ������
        /// </summary>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����Ƿ��������������ʵ��
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// �������
        /// </summary>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// ֻ���ִ��ӿ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <typeparam name="TKey">��������</typeparam>
    public interface IReadOnlyRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// ����������ȡʵ�壨����Ϊ�գ�
        /// </summary>
        Task<TEntity?> GetAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ�壨������ʱ�׳��쳣��
        /// </summary>
        Task<TEntity> GetRequiredAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ��ȡ����ʵ���б�
        /// </summary>
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ���б�
        /// </summary>
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// �����������ҵ�һ��ʵ��
        /// </summary>
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// ��ȡʵ������
        /// </summary>
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// ����������ȡʵ������
        /// </summary>
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

        /// <summary>
        /// ����Ƿ��������������ʵ��
        /// </summary>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// ֧�ֻ���Ĳִ��ӿ�
    /// </summary>
    /// <typeparam name="TEntity">ʵ������</typeparam>
    /// <typeparam name="TKey">��������</typeparam>
    public interface ICachedRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// �ӻ����л�ȡʵ��
        /// </summary>
        Task<TEntity?> GetFromCacheAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ʹ����ʵ�建��ʧЧ
        /// </summary>
        Task InvalidateCacheAsync(TKey id, CancellationToken cancellationToken = default);

        /// <summary>
        /// ʹ���ʵ�建��ʧЧ
        /// </summary>
        Task InvalidateCacheAsync(params TKey[] ids);

        /// <summary>
        /// ʹ����ʵ�建��ʧЧ
        /// </summary>
        ValueTask InvalidateAllCacheAsync(CancellationToken cancellationToken = default);
    }
}