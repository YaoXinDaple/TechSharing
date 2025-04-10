namespace RateLimiterExp.RateLimiter
{
    public class TokenBucketLimiter : IRateLimiter
    {
        private readonly int _capacity = 10; // 桶的最大容量
        private readonly int _refillRate = 2; // 每秒补充的令牌数
        private int _currentTokens; // 当前令牌数
        private DateTime _lastRefillTime; // 上次补充令牌的时间
        private readonly object _lock = new(); // 用于线程安全

        public TokenBucketLimiter()
        {
            _currentTokens = _capacity; // 初始化时桶是满的
            _lastRefillTime = DateTime.UtcNow;
        }

        public bool Acquire()
        {
            lock (_lock)
            {
                RefillTokens(); // 补充令牌

                if (_currentTokens > 0)
                {
                    _currentTokens--; // 消耗一个令牌
                    return true; // 请求成功
                }

                return false; // 请求被限流
            }
        }

        private void RefillTokens()
        {
            var now = DateTime.UtcNow;
            var timeElapsed = (now - _lastRefillTime).TotalSeconds;

            if (timeElapsed > 1)
            {
                // 根据时间间隔补充令牌
                var tokensToAdd = (int)(timeElapsed * _refillRate);
                _currentTokens = Math.Min(_capacity, _currentTokens + tokensToAdd);
                _lastRefillTime = now;
            }
        }

        public void ReturnToken()
        {
            lock (_lock)
            {
                // 如果当前令牌数小于桶的容量，则返还一个令牌
                if (_currentTokens < _capacity)
                {
                    _currentTokens++;
                }
            }
        }
    }
}
