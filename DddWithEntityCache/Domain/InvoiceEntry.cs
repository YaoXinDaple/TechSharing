namespace DddWithEntityCache.Domain
{
    public class InvoiceEntry
    {
        private InvoiceEntry() { }
        private InvoiceEntry(Guid invoiceId, string productName, decimal price, int quantity)
        {
            InvoiceId = invoiceId;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
        public Guid Id { get; init; }
        public Guid InvoiceId { get; init; }
        public string ProductName { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
        public decimal Total => Price * Quantity;
        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        internal class InvoiceEntryFactory
        {
            internal InvoiceEntry Create(Guid invoiceId, string productName, decimal price, int quantity)
            {
                if (invoiceId == Guid.Empty)
                    throw new ArgumentException("Invoice ID cannot be empty.", nameof(invoiceId));
                if (string.IsNullOrWhiteSpace(productName))
                    throw new ArgumentException("Product name cannot be empty.", nameof(productName));
                if (price < 0)
                    throw new ArgumentOutOfRangeException(nameof(price), "Price cannot be negative.");
                if (quantity <= 0)
                    throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
                
                return new InvoiceEntry(invoiceId, productName, price, quantity);
            }
        }
    }
}
