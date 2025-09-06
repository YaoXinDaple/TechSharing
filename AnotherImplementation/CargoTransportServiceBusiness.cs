namespace AnotherImplementation
{
    /// <summary>
    /// ������������ض�ҵ�����
    /// </summary>
    public class CargoTransportServiceBusiness
    {
        private readonly IGenericProperties _properties;

        public CargoTransportServiceBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// ��ʼ��ַ (Property1)
        /// </summary>
        public string? OriginAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// Ŀ�ĵ�ַ (Property2)
        /// </summary>
        public string? DestinationAddress
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// �������� (Property3)
        /// </summary>
        public string? CargoType
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// ���乫˾ (Property4)
        /// </summary>
        public string? TransportCompany
        {
            get => _properties.Property4;
            set => _properties.Property4 = value;
        }

        /// <summary>
        /// ˾������ (Property5)
        /// </summary>
        public string? DriverName
        {
            get => _properties.Property5;
            set => _properties.Property5 = value;
        }

        /// <summary>
        /// ������������� (IntProperty1)
        /// </summary>
        public int? CargoWeight
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// ������루��� (IntProperty2)
        /// </summary>
        public int? TransportDistance
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// �������� (IntProperty3)
        /// </summary>
        public int? VehicleCount
        {
            get => _properties.IntProperty3;
            set => _properties.IntProperty3 = value;
        }

        /// <summary>
        /// ������� (DecimalProperty1)
        /// </summary>
        public decimal? TransportFee
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// ȼ�ͷ� (DecimalProperty2)
        /// </summary>
        public decimal? FuelCost
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// ���շ� (DecimalProperty3)
        /// </summary>
        public decimal? InsuranceFee
        {
            get => _properties.DecimalProperty3;
            set => _properties.DecimalProperty3 = value;
        }

        /// <summary>
        /// �������� (DateProperty1)
        /// </summary>
        public DateTime? ShipmentDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// Ԥ�Ƶ������� (DateProperty2)
        /// </summary>
        public DateTime? EstimatedArrivalDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// �Ƿ���Ҫ��� (BoolProperty1)
        /// </summary>
        public bool? RequiresRefrigeration
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// �Ƿ�ΪΣ��Ʒ (BoolProperty2)
        /// </summary>
        public bool? IsHazardousMaterial
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}