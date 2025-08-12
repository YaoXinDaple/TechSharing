namespace DddWithEntityCache.Dto
{
    public class InvoiceDto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string SellerName { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public List<InvoiceEntryDto> Entries { get; set; } = new List<InvoiceEntryDto>();

    }
}
