namespace RateLimiterExp.RateLimiter
{
    public interface IRateLimiter
    {
        /// <summary>
        /// 消耗令牌
        /// </summary>
        /// <returns></returns>
        bool Acquire();

        /// <summary>
        /// 返还令牌
        /// </summary>
        void ReturnToken();
    }
}
