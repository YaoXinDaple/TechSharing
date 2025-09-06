namespace AnotherImplementation
{
    /// <summary>
    /// ���������ض�ҵ�����
    /// </summary>
    public class HouseRentalBusiness
    {
        private readonly IGenericProperties _properties;

        public HouseRentalBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// ���ݵ�ַ (Property1)
        /// </summary>
        public string? HouseAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// �������� (Property2)
        /// </summary>
        public string? RentalPeriod
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// �������� (Property3)
        /// </summary>
        public string? LandlordName
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// ���������ƽ���ף� (IntProperty1)
        /// </summary>
        public int? HouseArea
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// �������� (IntProperty2)
        /// </summary>
        public int? RoomCount
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// ����� (DecimalProperty1)
        /// </summary>
        public decimal? MonthlyRent
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// Ѻ�� (DecimalProperty2)
        /// </summary>
        public decimal? Deposit
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// ���޿�ʼ���� (DateProperty1)
        /// </summary>
        public DateTime? RentalStartDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// ���޽������� (DateProperty2)
        /// </summary>
        public DateTime? RentalEndDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// �Ƿ�����Ҿ� (BoolProperty1)
        /// </summary>
        public bool? IncludesFurniture
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// �Ƿ�������� (BoolProperty2)
        /// </summary>
        public bool? AllowsPets
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}