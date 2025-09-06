namespace AnotherImplementation
{
    /// <summary>
    /// ��Ʊͨ����Ϣ DTO
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
        /// �ض�ҵ�������Ϣ
        /// </summary>
        public object? SpecificBusinessData { get; set; }
    }
}