namespace OutboxExampleAPI.DTOs
{
    public class InvoiceModel
    {
        public string OrderNo { get; set; }

        public Guid Id { get; set; }

        public string BuyerName { get; set; }

        public List<StandardEntryModel> Entries { get; set; }

    }
}
