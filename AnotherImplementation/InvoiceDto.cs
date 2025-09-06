namespace AnotherImplementation
{
    /// <summary>
    /// 发票通用信息 DTO
    /// </summary>
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public SpecificBusinessType BusinessType { get; set; }
        
        /// <summary>
        /// 特定业务对象信息
        /// </summary>
        public object? SpecificBusinessData { get; set; }
    }
}