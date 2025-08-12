using Newtonsoft.Json;

namespace DddWithEntityCache.Domain
{
    public class Invoice
    {
        private Invoice() { }

        [JsonConstructor]
        private Invoice(Guid id, string customerName, string sellerName, decimal amount, List<InvoiceEntry> entries)
        {
            Id = id;
            CustomerName = customerName;
            SellerName = sellerName;
            Amount = amount;
            _entries = entries;
        }
        public Guid Id { get; init; }
        public string CustomerName { get; private set; } = string.Empty;
        public string SellerName { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }

        internal void UpdateCustomerName(string customerName)
        {
            CustomerName = customerName;
        }

        internal void UpdateSellerName(string sellerName)
        {
            SellerName = sellerName;
        }

        public IReadOnlyCollection<InvoiceEntry> Entries => _entries.AsReadOnly();

        private readonly List<InvoiceEntry> _entries = new();


        internal class InvoiceFactory
        { 
            internal Invoice Create(Guid id, string customerName, string sellerName, decimal amount, List<InvoiceEntry> entries)
            {
                if (id == Guid.Empty)
                    throw new ArgumentException("Id cannot be empty.", nameof(id));
                if (string.IsNullOrWhiteSpace(customerName))
                    throw new ArgumentException("Customer name cannot be empty.", nameof(customerName));
                if (string.IsNullOrWhiteSpace(sellerName))
                    throw new ArgumentException("Seller name cannot be empty.", nameof(sellerName));
                if (amount < 0)
                    throw new ArgumentOutOfRangeException(nameof(amount), "Amount cannot be negative.");
                if (entries == null || !entries.Any())
                    throw new ArgumentException("Entries cannot be null or empty.", nameof(entries));
                return new Invoice(id, customerName, sellerName, amount, entries);
            }
        }

    }
}
