using DddWithEntityCache.Domain;
using Microsoft.EntityFrameworkCore;
using ZiggyCreatures.Caching.Fusion;

namespace DddWithEntityCache.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceDbContext _context;
        private readonly ILogger<InvoiceRepository> _logger;
        private readonly IFusionCache _fusionCache;
        private const string CacheKeyPrefix = "Invoice_";
        private static TimeSpan CacheDuration = TimeSpan.FromMinutes(30);

        public InvoiceRepository(InvoiceDbContext context, 
            ILogger<InvoiceRepository> logger, 
            IFusionCache fusionCache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger;
            _fusionCache = fusionCache;
        }

        private static string GetCacheKey(Guid id) => $"{CacheKeyPrefix}{id}";

        public async Task DeleteAsync(Invoice invoice)
        {
            if (invoice == null) throw new ArgumentNullException(nameof(invoice));

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            // 删除缓存
            await _fusionCache.RemoveAsync(GetCacheKey(invoice.Id));
            _logger.LogInformation($"Removed invoice {invoice.Id} from cache after deletion");
        }

        public async Task<Invoice> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid invoice ID.", nameof(id));

            // 从缓存获取，如果缓存未命中则从数据库获取
            var invoice = await _fusionCache.GetOrSetAsync(
                GetCacheKey(id),
                async (ctx) => await FetchInvoiceAsync(id),
                options => options
                    .SetDuration(CacheDuration)
                    .SetFailSafe(true, TimeSpan.FromMinutes(5)) // 添加失败安全机制
            );

            return invoice ?? throw new KeyNotFoundException($"Invoice with ID {id} not found.");
        }

        private async Task<Invoice?> FetchInvoiceAsync(Guid id)
        {
            _logger.LogInformation($"Fetching invoice {id} from database.");
            return await _context.Invoices
                .Include(i => i.Entries)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task InsertAsync(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            // 添加到缓存
            await _fusionCache.SetAsync(
                GetCacheKey(invoice.Id),
                invoice,
                options => options.SetDuration(CacheDuration)
            );
            _logger.LogInformation($"Added invoice {invoice.Id} to cache after insertion");
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            _context.Invoices.Update(invoice);
            await _context.SaveChangesAsync();

            // 更新缓存
            await _fusionCache.SetAsync(
                GetCacheKey(invoice.Id),
                invoice,
                options => options.SetDuration(CacheDuration)
            );
            _logger.LogInformation($"Updated invoice {invoice.Id} in cache after update");
        }

        // 可选：批量清除缓存的方法
        public async Task InvalidateCacheAsync(params Guid[] invoiceIds)
        {
            foreach (var id in invoiceIds)
            {
                await _fusionCache.RemoveAsync(GetCacheKey(id));
            }
            _logger.LogInformation($"Invalidated cache for {invoiceIds.Length} invoices");
        }

        // 可选：清除所有发票缓存的方法
        public async Task InvalidateAllCacheAsync()
        {
            // 注意：这个方法需要 FusionCache 支持模式匹配的删除操作
            await _fusionCache.RemoveByTagAsync(CacheKeyPrefix);
            _logger.LogInformation("Invalidated all invoice caches");
        }
    }
}
