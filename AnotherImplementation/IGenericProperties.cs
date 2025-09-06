namespace AnotherImplementation
{
    /// <summary>
    /// 通用属性接口，定义了通用的属性字段
    /// </summary>
    public interface IGenericProperties
    {
        // String 类型属性
        string? Property1 { get; set; }
        string? Property2 { get; set; }
        string? Property3 { get; set; }
        string? Property4 { get; set; }
        string? Property5 { get; set; }

        // Int 类型属性
        int? IntProperty1 { get; set; }
        int? IntProperty2 { get; set; }
        int? IntProperty3 { get; set; }

        // Decimal 类型属性
        decimal? DecimalProperty1 { get; set; }
        decimal? DecimalProperty2 { get; set; }
        decimal? DecimalProperty3 { get; set; }

        // Date 类型属性
        DateTime? DateProperty1 { get; set; }
        DateTime? DateProperty2 { get; set; }

        // Bool 类型属性
        bool? BoolProperty1 { get; set; }
        bool? BoolProperty2 { get; set; }
    }
}