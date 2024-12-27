namespace OutboxExampleAPI.DTOs
{
    public class StandardEntryModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }

        public string ItemName { get; set; }
        public decimal Amount { get; set; }
        public bool IsDiscountLine { get; set; }
    }
}
