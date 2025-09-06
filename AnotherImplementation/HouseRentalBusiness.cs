namespace AnotherImplementation
{
    /// <summary>
    /// 房屋租赁特定业务对象
    /// </summary>
    public class HouseRentalBusiness
    {
        private readonly IGenericProperties _properties;

        public HouseRentalBusiness(IGenericProperties properties)
        {
            _properties = properties;
        }

        /// <summary>
        /// 房屋地址 (Property1)
        /// </summary>
        public string? HouseAddress
        {
            get => _properties.Property1;
            set => _properties.Property1 = value;
        }

        /// <summary>
        /// 租赁期限 (Property2)
        /// </summary>
        public string? RentalPeriod
        {
            get => _properties.Property2;
            set => _properties.Property2 = value;
        }

        /// <summary>
        /// 房东姓名 (Property3)
        /// </summary>
        public string? LandlordName
        {
            get => _properties.Property3;
            set => _properties.Property3 = value;
        }

        /// <summary>
        /// 房屋面积（平方米） (IntProperty1)
        /// </summary>
        public int? HouseArea
        {
            get => _properties.IntProperty1;
            set => _properties.IntProperty1 = value;
        }

        /// <summary>
        /// 房间数量 (IntProperty2)
        /// </summary>
        public int? RoomCount
        {
            get => _properties.IntProperty2;
            set => _properties.IntProperty2 = value;
        }

        /// <summary>
        /// 月租金 (DecimalProperty1)
        /// </summary>
        public decimal? MonthlyRent
        {
            get => _properties.DecimalProperty1;
            set => _properties.DecimalProperty1 = value;
        }

        /// <summary>
        /// 押金 (DecimalProperty2)
        /// </summary>
        public decimal? Deposit
        {
            get => _properties.DecimalProperty2;
            set => _properties.DecimalProperty2 = value;
        }

        /// <summary>
        /// 租赁开始日期 (DateProperty1)
        /// </summary>
        public DateTime? RentalStartDate
        {
            get => _properties.DateProperty1;
            set => _properties.DateProperty1 = value;
        }

        /// <summary>
        /// 租赁结束日期 (DateProperty2)
        /// </summary>
        public DateTime? RentalEndDate
        {
            get => _properties.DateProperty2;
            set => _properties.DateProperty2 = value;
        }

        /// <summary>
        /// 是否包含家具 (BoolProperty1)
        /// </summary>
        public bool? IncludesFurniture
        {
            get => _properties.BoolProperty1;
            set => _properties.BoolProperty1 = value;
        }

        /// <summary>
        /// 是否允许宠物 (BoolProperty2)
        /// </summary>
        public bool? AllowsPets
        {
            get => _properties.BoolProperty2;
            set => _properties.BoolProperty2 = value;
        }
    }
}