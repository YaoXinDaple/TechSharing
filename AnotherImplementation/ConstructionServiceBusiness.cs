namespace AnotherImplementation
{
    /// <summary>
    /// 建筑服务特定业务对象
    /// </summary>
    public class ConstructionServiceBusiness
    {
        private readonly IGenericProperties _properties;

        public ConstructionServiceBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// 项目地址 (Property1)
        /// </summary>
        public string? ProjectAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// 服务类型 (Property2)
        /// </summary>
        public string? ServiceType
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// 承包商名称 (Property3)
        /// </summary>
        public string? ContractorName
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// 项目经理 (Property4)
        /// </summary>
        public string? ProjectManager
        {
            get => _properties.Property4;
            set => _properties.Property4 = value;
        }

        /// <summary>
        /// 施工面积（平方米） (IntProperty1)
        /// </summary>
        public int? ConstructionArea
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// 预计工期（天） (IntProperty2)
        /// </summary>
        public int? EstimatedDuration
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// 工人数量 (IntProperty3)
        /// </summary>
        public int? WorkerCount
        {
            get => _properties.IntProperty3;
            set => _properties.IntProperty3 = value;
        }

        /// <summary>
        /// 材料费用 (DecimalProperty1)
        /// </summary>
        public decimal? MaterialCost
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// 人工费用 (DecimalProperty2)
        /// </summary>
        public decimal? LaborCost
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// 设备费用 (DecimalProperty3)
        /// </summary>
        public decimal? EquipmentCost
        {
            get => _properties.DecimalProperty3;
            set => _properties.DecimalProperty3 = value;
        }

        /// <summary>
        /// 开工日期 (DateProperty1)
        /// </summary>
        public DateTime? StartDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// 完工日期 (DateProperty2)
        /// </summary>
        public DateTime? CompletionDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// 是否需要许可证 (BoolProperty1)
        /// </summary>
        public bool? RequiresPermit
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// 是否为紧急工程 (BoolProperty2)
        /// </summary>
        public bool? IsEmergencyProject
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}