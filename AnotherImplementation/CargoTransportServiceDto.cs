namespace AnotherImplementation
{
    /// <summary>
    /// 货物运输服务业务 DTO
    /// </summary>
    public class CargoTransportServiceDto
    {
        public string? OriginAddress { get; set; }
        public string? DestinationAddress { get; set; }
        public string? CargoType { get; set; }
        public string? TransportCompany { get; set; }
        public string? DriverName { get; set; }
        public int? CargoWeight { get; set; }
        public int? TransportDistance { get; set; }
        public int? VehicleCount { get; set; }
        public decimal? TransportFee { get; set; }
        public decimal? FuelCost { get; set; }
        public decimal? InsuranceFee { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public DateTime? EstimatedArrivalDate { get; set; }
        public bool? RequiresRefrigeration { get; set; }
        public bool? IsHazardousMaterial { get; set; }
    }
}