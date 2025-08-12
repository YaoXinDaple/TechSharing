namespace DddWithEntityCache.Domain
{
    public interface IInvoiceRepository
    {
        public Task<Invoice> GetAsync(Guid id);

        public Task InsertAsync(Invoice invoice);

        public Task UpdateAsync(Invoice invoice);

        public Task DeleteAsync(Invoice invoice);
    }
}
