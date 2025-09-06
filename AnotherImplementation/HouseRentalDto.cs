namespace AnotherImplementation
{
    /// <summary>
    /// ·¿ÎÝ×âÁÞÒµÎñ DTO
    /// </summary>
    public class HouseRentalDto
    {
        public string? HouseAddress { get; set; }
        public string? RentalPeriod { get; set; }
        public string? LandlordName { get; set; }
        public int? HouseArea { get; set; }
        public int? RoomCount { get; set; }
        public decimal? MonthlyRent { get; set; }
        public decimal? Deposit { get; set; }
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }
        public bool? IncludesFurniture { get; set; }
        public bool? AllowsPets { get; set; }
    }
}