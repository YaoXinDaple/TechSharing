namespace AnotherImplementation
{
    /// <summary>
    /// ͨ�����Խӿڣ�������ͨ�õ������ֶ�
    /// </summary>
    public interface IGenericProperties
    {
        // String ��������
        string? Property1 { get; set; }
        string? Property2 { get; set; }
        string? Property3 { get; set; }
        string? Property4 { get; set; }
        string? Property5 { get; set; }

        // Int ��������
        int? IntProperty1 { get; set; }
        int? IntProperty2 { get; set; }
        int? IntProperty3 { get; set; }

        // Decimal ��������
        decimal? DecimalProperty1 { get; set; }
        decimal? DecimalProperty2 { get; set; }
        decimal? DecimalProperty3 { get; set; }

        // Date ��������
        DateTime? DateProperty1 { get; set; }
        DateTime? DateProperty2 { get; set; }

        // Bool ��������
        bool? BoolProperty1 { get; set; }
        bool? BoolProperty2 { get; set; }
    }
}