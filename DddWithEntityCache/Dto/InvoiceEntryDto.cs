namespace DddWithEntityCache.Dto
{
    public class InvoiceEntryDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
