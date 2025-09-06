# AnotherImplementation - ��Ʊҵ�����ӳ��������

## ��Ŀ����

����Ŀʵ����һ�����ķ�Ʊҵ�����ӳ����������ͨ��ͨ�������ֶ���֧�ֶ����ض�ҵ�����ͣ�������Ϊÿ��ҵ�����͵����������ݿ��ֶεĸ����ԡ�

## �ܹ����

### �������

1. **IGenericProperties �ӿ�** - ����ͨ�������ֶ�
   - String �������� (Property1-Property5)
   - Int �������� (IntProperty1-IntProperty3)
   - Decimal �������� (DecimalProperty1-DecimalProperty3)
   - DateTime �������� (DateProperty1-DateProperty2)
   - Bool �������� (BoolProperty1-BoolProperty2)

2. **SpecificBusinessType ö��** - ����֧�ֵ�ҵ������
   - HouseRental (��������)
   - ConstructionService (��������)
   - CargoTransportService (�����������)

3. **Invoice ʵ����** - ��Ʊ����ʵ��
   - ʵ�� IGenericProperties �ӿ�
   - ����ͨ�÷�Ʊ�ֶ�
   - �ṩ GetTyped() ���������ض�ҵ�����

4. **�ض�ҵ�������**
   - HouseRentalBusiness - ��������ҵ��
   - ConstructionServiceBusiness - ��������ҵ��
   - CargoTransportServiceBusiness - �����������ҵ��

5. **DTO ��ϵ��**
   - InvoiceDto - ��Ʊͨ�� DTO
   - HouseRentalDto - �������� DTO
   - ConstructionServiceDto - �������� DTO
   - CargoTransportServiceDto - ����������� DTO

6. **InvoiceMapper ӳ����** - ����ʵ���� DTO ֮���ת��

## ʹ��ʾ��

### �����������޷�Ʊ

```csharp
var invoice = new Invoice
{
    InvoiceNumber = "INV-2024-001",
    CustomerName = "����",
    BusinessType = SpecificBusinessType.HouseRental
};

// ͨ���ض�ҵ�������������
if (invoice.GetTyped() is HouseRentalBusiness houseRental)
{
    houseRental.HouseAddress = "�����г�����xxxС��";
    houseRental.MonthlyRent = 5000m;
    houseRental.HouseArea = 80;
}

// ת��Ϊ DTO
var dto = InvoiceMapper.ToDto(invoice);
```

## ��Ҫ����

1. **�����** - ͨ��ͨ�������ֶ�֧�ֶ���ҵ������
2. **����չ��** - ��������µ�ҵ������
3. **���Ͱ�ȫ** - �ض�ҵ������ṩǿ�������Է���
4. **����ӳ��** - ͨ�������ֶ������ҵ���ֶε���ȷӳ���ϵ
5. **��һ����** - ÿ���඼����ȷ��ְ��

## �����ֶ�ӳ��ʾ��

### ��������ҵ��
- Property1 �� HouseAddress (���ݵ�ַ)
- Property2 �� RentalPeriod (��������)
- Property3 �� LandlordName (��������)
- IntProperty1 �� HouseArea (�������)
- DecimalProperty1 �� MonthlyRent (�����)
- DateProperty1 �� RentalStartDate (���޿�ʼ����)
- BoolProperty1 �� IncludesFurniture (�Ƿ�����Ҿ�)

### ��������ҵ��
- Property1 �� ProjectAddress (��Ŀ��ַ)
- Property2 �� ServiceType (��������)
- Property3 �� ContractorName (�а�������)
- IntProperty1 �� ConstructionArea (ʩ�����)
- DecimalProperty1 �� MaterialCost (���Ϸ���)
- DateProperty1 �� StartDate (��������)
- BoolProperty1 �� RequiresPermit (�Ƿ���Ҫ���֤)

### �����������ҵ��
- Property1 �� OriginAddress (��ʼ��ַ)
- Property2 �� DestinationAddress (Ŀ�ĵ�ַ)
- Property3 �� CargoType (��������)
- IntProperty1 �� CargoWeight (��������)
- DecimalProperty1 �� TransportFee (�������)
- DateProperty1 �� ShipmentDate (��������)
- BoolProperty1 �� RequiresRefrigeration (�Ƿ���Ҫ���)

## ����ջ

- .NET 9
- C# �����﷨����
- ����������ԭ��

## ���ģʽ

- **���ģʽ** - �ض�ҵ�����ͨ����Ϸ�ʽ����ͨ�����Խӿ�
- **����ģʽ** - ͨ��ҵ������ö��ȷ��ʹ�õ�ӳ�����
- **����ģʽ** - GetTyped() ��������ҵ�����ʹ�����Ӧ��ҵ�����