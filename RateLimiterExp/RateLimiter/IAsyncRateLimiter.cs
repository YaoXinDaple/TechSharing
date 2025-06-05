namespace RateLimiterExp.RateLimiter
{
    public interface IAsyncRateLimiter
    {
        /// <summary>
        /// 异步消耗令牌
        /// </summary>
        /// <returns></returns>
        Task<bool> AcquireAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 异步返还令牌
        /// </summary>
        Task ReturnTokenAsync(CancellationToken cancellationToken = default);
    }
}
