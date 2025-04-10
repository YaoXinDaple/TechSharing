
namespace RateLimiterExp.RateLimiter
{
    public class TokenBucketLimiterAsynchronous : IAsyncRateLimiter
    {
        private readonly int _capacity = 10; // 桶的最大容量
        private readonly int _refillRate = 2; // 每秒补充的令牌数
        private int _currentTokens; // 当前令牌数
        private DateTime _lastRefillTime; // 上次补充令牌的时间
        private readonly SemaphoreSlim _semaphore = new(1, 1); // 用于异步线程同步

        public TokenBucketLimiterAsynchronous()
        {
            _currentTokens = _capacity; // 初始化时桶是满的
            _lastRefillTime = DateTime.UtcNow;
        }


        public async Task<bool> AcquireAsync(CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken); // 异步等待
            try
            {
                RefillTokens(); // 补充令牌

                if (_currentTokens > 0)
                {
                    _currentTokens--; // 消耗一个令牌
                    return true; // 请求成功
                }

                return false; // 请求被限流
            }
            finally
            {
                _semaphore.Release();
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
            _semaphore.Wait(); // 同步等待
            try
            {
                // 如果当前令牌数小于桶的容量，则返还一个令牌
                if (_currentTokens < _capacity)
                {
                    _currentTokens++;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task ReturnTokenAsync(CancellationToken cancellationToken = default)
        {
            await _semaphore.WaitAsync(cancellationToken); // 异步等待
            try
            {
                // 如果当前令牌数小于桶的容量，则返还一个令牌
                if (_currentTokens < _capacity)
                {
                    _currentTokens++;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

    }
}
