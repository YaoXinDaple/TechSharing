namespace AnotherImplementation
{
    /// <summary>
    /// 货物运输服务特定业务对象
    /// </summary>
    public class CargoTransportServiceBusiness
    {
        private readonly IGenericProperties _properties;

        public CargoTransportServiceBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// 起始地址 (Property1)
        /// </summary>
        public string? OriginAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// 目的地址 (Property2)
        /// </summary>
        public string? DestinationAddress
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// 货物类型 (Property3)
        /// </summary>
        public string? CargoType
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// 运输公司 (Property4)
        /// </summary>
        public string? TransportCompany
        {
            get => _properties.Property4;
            set => _properties.Property4 = value;
        }

        /// <summary>
        /// 司机姓名 (Property5)
        /// </summary>
        public string? DriverName
        {
            get => _properties.Property5;
            set => _properties.Property5 = value;
        }

        /// <summary>
        /// 货物重量（公斤） (IntProperty1)
        /// </summary>
        public int? CargoWeight
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// 运输距离（公里） (IntProperty2)
        /// </summary>
        public int? TransportDistance
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// 车辆数量 (IntProperty3)
        /// </summary>
        public int? VehicleCount
        {
            get => _properties.IntProperty3;
            set => _properties.IntProperty3 = value;
        }

        /// <summary>
        /// 运输费用 (DecimalProperty1)
        /// </summary>
        public decimal? TransportFee
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// 燃油费 (DecimalProperty2)
        /// </summary>
        public decimal? FuelCost
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// 保险费 (DecimalProperty3)
        /// </summary>
        public decimal? InsuranceFee
        {
            get => _properties.DecimalProperty3;
            set => _properties.DecimalProperty3 = value;
        }

        /// <summary>
        /// 发货日期 (DateProperty1)
        /// </summary>
        public DateTime? ShipmentDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// 预计到达日期 (DateProperty2)
        /// </summary>
        public DateTime? EstimatedArrivalDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// 是否需要冷藏 (BoolProperty1)
        /// </summary>
        public bool? RequiresRefrigeration
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// 是否为危险品 (BoolProperty2)
        /// </summary>
        public bool? IsHazardousMaterial
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}