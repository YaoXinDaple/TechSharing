# �������ϵͳʵ��

## ����

�����Ŀʵ����һ����������ϵͳ�����ڴ���ͬҵ�������µ��ض����������Է�ƱΪ������ͬҵ�����ͣ��������ޡ��������񡢻����������ȣ��ķ�Ʊ������Ҫ��ͬ���ض����ԣ�������ͨ��ͨ�������ֶκ�����ϵͳ���������ݿ��ṹ�ĸ��ӻ���

## ���˼·

### ���ĸ���

1. **ͨ�������ֶ�**: ����ʵ���ࣨInvoice���ж������ɸ�ͨ�õ������ֶΣ�������
   - `IntProperty1-3`: ������������
   - `StringProperty1-5`: �ַ�����������
   - `DecimalProperty1-3`: С����������
   - `DateProperty1-2`: ������������
   - `BoolProperty1-2`: ������������

2. **ҵ������ö��**: ���岻ͬ��ҵ�����������־����ҵ�񳡾�

3. **����ϵͳ**: ͨ�����ñ���ÿ��ҵ����������Щͨ�������ֶεľ��庬��

4. **���ͻ�����**: ʵ�� `GetTyped()` ���������ؾ��о���ҵ������ض�ҵ�����

## �ܹ����

### 1. ö�ٶ���

```csharp
// ҵ������
public enum BusinessType
{
    HouseRental,        // ��������
    ConstructionService, // ��������
    TransportService    // �����������
}

// ��������
public enum PropertyType
{
    Int, String, Decimal, Date, Bool
}
```

### 2. ����ϵͳ

- `PropertyConfiguration`: ���嵥�����Ե�������Ϣ
- `BusinessTypeConfiguration`: �����ض�ҵ�����͵�������������
- `PropertyConfigurationManager`: ��������������Ϣ�ľ�̬��

### 3. ��ʵ����

`Invoice` �������
- ������Ʊ��Ϣ��ID����Ʊ�š�ҵ�����͡����ڡ���
- ͨ�������ֶ�
- `GetTyped()` ���������ض���ҵ�����

### 4. �ض�ҵ�����

- `ITypedInvoice`: �ض�ҵ������ͨ�ýӿ�
- `HouseRentalInvoice`: �������޷�Ʊ
- `ConstructionServiceInvoice`: ��������Ʊ
- `TransportServiceInvoice`: �����������Ʊ

## ʹ��ʾ��

### �����������޷�Ʊ

```csharp
var houseRentalInvoice = new Invoice
{
    Id = 1,
    InvoiceNumber = "INV-2024-001",
    BusinessType = BusinessType.HouseRental,
    IssueDate = DateTime.Now,
    Amount = 5000m,
    StringProperty1 = "�����г����������ֵ�1��", // ������ַ
    DecimalProperty1 = 120.5m,                   // �������
    DateProperty1 = new DateTime(2024, 1, 1),    // ���޿�ʼ����
    DateProperty2 = new DateTime(2024, 12, 31),  // ���޽�������
    BoolProperty1 = true,                        // ����ˮ���
    IntProperty1 = 3,                            // ��������
    StringProperty2 = "����",                     // ��������
    StringProperty3 = "����"                      // �������
};

// ��ȡ�ض�ҵ�����
var typedHouseRental = houseRentalInvoice.GetTyped() as HouseRentalInvoice;
Console.WriteLine($"������ַ: {typedHouseRental.PropertyAddress}");
Console.WriteLine($"�������: {typedHouseRental.RentalArea} ƽ����");
```

### �������ò�ѯ

```csharp
// ��ȡҵ����������
var config = PropertyConfigurationManager.GetConfiguration(BusinessType.HouseRental);

// ��ȡ�ض���������
var addressConfig = PropertyConfigurationManager.GetPropertyConfiguration(
    BusinessType.HouseRental, "StringProperty1");
Console.WriteLine($"StringProperty1 �ĺ���: {addressConfig.DisplayName}");
```

## ����

1. **���ݿ���Ƽ�**: ����Ϊÿ��ҵ�����ʹ����������ֶ�
2. **����Ը�**: ����ͨ������ϵͳ��������µ�ҵ������
3. **���Ͱ�ȫ**: ͨ��ǿ���͵��ض�ҵ������ṩ����ʱ���
4. **��ά���Ժ�**: ���ü��й�������ά������չ
5. **������**: ����ҵ�����Ͳ�Ӱ�����д���

## ��չ��

### �����ҵ������

1. �� `BusinessType` ö�������������
2. �����µ��ض�ҵ�������
3. �� `PropertyConfigurationManager` ���������
4. �� `Invoice.GetTyped()` �����������Ӧ�� case

### �������������

�����Ҫ���������ֶΣ������� `Invoice` ������Ӹ���ͨ�����ԣ�����Ӧ��������ϵͳ��

## ע������

1. �����ֶ��������ޣ���Ҫ����ʵ��ҵ���������滮
2. ������Ϣ����洢�����ݿ��У�������Ӳ����
3. �������������֤����ȷ������������
4. ���Կ����������ӳ��Ļ�������������

## ����ջ

- .NET 9
- C# 13
- ʹ���� nullable reference types ȷ�����밲ȫ��
- ʹ���� required ����ȷ���ؼ��ֶβ�Ϊ��