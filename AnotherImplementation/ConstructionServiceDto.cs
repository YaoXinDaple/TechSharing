namespace AnotherImplementation
{
    /// <summary>
    /// 建筑服务业务 DTO
    /// </summary>
    public class ConstructionServiceDto
    {
        public string? ProjectAddress { get; set; }
        public string? ServiceType { get; set; }
        public string? ContractorName { get; set; }
        public string? ProjectManager { get; set; }
        public int? ConstructionArea { get; set; }
        public int? EstimatedDuration { get; set; }
        public int? WorkerCount { get; set; }
        public decimal? MaterialCost { get; set; }
        public decimal? LaborCost { get; set; }
        public decimal? EquipmentCost { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool? RequiresPermit { get; set; }
        public bool? IsEmergencyProject { get; set; }
    }
}