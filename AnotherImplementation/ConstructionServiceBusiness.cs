namespace AnotherImplementation
{
    /// <summary>
    /// ���������ض�ҵ�����
    /// </summary>
    public class ConstructionServiceBusiness
    {
        private readonly IGenericProperties _properties;

        public ConstructionServiceBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// ��Ŀ��ַ (Property1)
        /// </summary>
        public string? ProjectAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// �������� (Property2)
        /// </summary>
        public string? ServiceType
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// �а������� (Property3)
        /// </summary>
        public string? ContractorName
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// ��Ŀ���� (Property4)
        /// </summary>
        public string? ProjectManager
        {
            get => _properties.Property4;
            set => _properties.Property4 = value;
        }

        /// <summary>
        /// ʩ�������ƽ���ף� (IntProperty1)
        /// </summary>
        public int? ConstructionArea
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// Ԥ�ƹ��ڣ��죩 (IntProperty2)
        /// </summary>
        public int? EstimatedDuration
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// �������� (IntProperty3)
        /// </summary>
        public int? WorkerCount
        {
            get => _properties.IntProperty3;
            set => _properties.IntProperty3 = value;
        }

        /// <summary>
        /// ���Ϸ��� (DecimalProperty1)
        /// </summary>
        public decimal? MaterialCost
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// �˹����� (DecimalProperty2)
        /// </summary>
        public decimal? LaborCost
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// �豸���� (DecimalProperty3)
        /// </summary>
        public decimal? EquipmentCost
        {
            get => _properties.DecimalProperty3;
            set => _properties.DecimalProperty3 = value;
        }

        /// <summary>
        /// �������� (DateProperty1)
        /// </summary>
        public DateTime? StartDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// �깤���� (DateProperty2)
        /// </summary>
        public DateTime? CompletionDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// �Ƿ���Ҫ���֤ (BoolProperty1)
        /// </summary>
        public bool? RequiresPermit
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// �Ƿ�Ϊ�������� (BoolProperty2)
        /// </summary>
        public bool? IsEmergencyProject
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}