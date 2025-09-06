namespace AnotherImplementation
{
    /// <summary>
    /// 发票实体类
    /// </summary>
    public class Invoice : IGenericProperties
    {
        // 发票通用字段
        public int Id { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public SpecificBusinessType BusinessType { get; set; }

        // 实现 IGenericProperties 接口
        public string? Property1 { get; set; }
        public string? Property2 { get; set; }
        public string? Property3 { get; set; }
        public string? Property4 { get; set; }
        public string? Property5 { get; set; }
        public int? IntProperty1 { get; set; }
        public int? IntProperty2 { get; set; }
        public int? IntProperty3 { get; set; }
        public decimal? DecimalProperty1 { get; set; }
        public decimal? DecimalProperty2 { get; set; }
        public decimal? DecimalProperty3 { get; set; }
        public DateTime? DateProperty1 { get; set; }
        public DateTime? DateProperty2 { get; set; }
        public bool? BoolProperty1 { get; set; }
        public bool? BoolProperty2 { get; set; }

        /// <summary>
        /// 获取特定业务对象
        /// </summary>
        /// <returns>特定业务对象或null</returns>
        public object? GetTyped()
        {
            return BusinessType switch
            {
                SpecificBusinessType.HouseRental => new HouseRentalBusiness(this),
                SpecificBusinessType.ConstructionService => new ConstructionServiceBusiness(this),
                SpecificBusinessType.CargoTransportService => new CargoTransportServiceBusiness(this),
                _ => null
            };
        }
    }
}