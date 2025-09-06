# AnotherImplementation - 发票业务对象映射解决方案

## 项目概述

本项目实现了一个灵活的发票业务对象映射解决方案，通过通用属性字段来支持多种特定业务类型，避免了为每种业务类型单独创建数据库字段的复杂性。

## 架构设计

### 核心组件

1. **IGenericProperties 接口** - 定义通用属性字段
   - String 类型属性 (Property1-Property5)
   - Int 类型属性 (IntProperty1-IntProperty3)
   - Decimal 类型属性 (DecimalProperty1-DecimalProperty3)
   - DateTime 类型属性 (DateProperty1-DateProperty2)
   - Bool 类型属性 (BoolProperty1-BoolProperty2)

2. **SpecificBusinessType 枚举** - 定义支持的业务类型
   - HouseRental (房屋租赁)
   - ConstructionService (建筑服务)
   - CargoTransportService (货物运输服务)

3. **Invoice 实体类** - 发票核心实体
   - 实现 IGenericProperties 接口
   - 包含通用发票字段
   - 提供 GetTyped() 方法返回特定业务对象

4. **特定业务对象类**
   - HouseRentalBusiness - 房屋租赁业务
   - ConstructionServiceBusiness - 建筑服务业务
   - CargoTransportServiceBusiness - 货物运输服务业务

5. **DTO 类系列**
   - InvoiceDto - 发票通用 DTO
   - HouseRentalDto - 房屋租赁 DTO
   - ConstructionServiceDto - 建筑服务 DTO
   - CargoTransportServiceDto - 货物运输服务 DTO

6. **InvoiceMapper 映射器** - 负责实体与 DTO 之间的转换

## 使用示例

### 创建房屋租赁发票

```csharp
var invoice = new Invoice
{
    InvoiceNumber = "INV-2024-001",
    CustomerName = "张三",
    BusinessType = SpecificBusinessType.HouseRental
};

// 通过特定业务对象设置属性
if (invoice.GetTyped() is HouseRentalBusiness houseRental)
{
    houseRental.HouseAddress = "北京市朝阳区xxx小区";
    houseRental.MonthlyRent = 5000m;
    houseRental.HouseArea = 80;
}

// 转换为 DTO
var dto = InvoiceMapper.ToDto(invoice);
```

## 主要特性

1. **灵活性** - 通过通用属性字段支持多种业务类型
2. **可扩展性** - 易于添加新的业务类型
3. **类型安全** - 特定业务对象提供强类型属性访问
4. **清晰映射** - 通用属性字段与具体业务字段的明确映射关系
5. **单一责任** - 每个类都有明确的职责

## 属性字段映射示例

### 房屋租赁业务
- Property1 → HouseAddress (房屋地址)
- Property2 → RentalPeriod (租赁期限)
- Property3 → LandlordName (房东姓名)
- IntProperty1 → HouseArea (房屋面积)
- DecimalProperty1 → MonthlyRent (月租金)
- DateProperty1 → RentalStartDate (租赁开始日期)
- BoolProperty1 → IncludesFurniture (是否包含家具)

### 建筑服务业务
- Property1 → ProjectAddress (项目地址)
- Property2 → ServiceType (服务类型)
- Property3 → ContractorName (承包商名称)
- IntProperty1 → ConstructionArea (施工面积)
- DecimalProperty1 → MaterialCost (材料费用)
- DateProperty1 → StartDate (开工日期)
- BoolProperty1 → RequiresPermit (是否需要许可证)

### 货物运输服务业务
- Property1 → OriginAddress (起始地址)
- Property2 → DestinationAddress (目的地址)
- Property3 → CargoType (货物类型)
- IntProperty1 → CargoWeight (货物重量)
- DecimalProperty1 → TransportFee (运输费用)
- DateProperty1 → ShipmentDate (发货日期)
- BoolProperty1 → RequiresRefrigeration (是否需要冷藏)

## 技术栈

- .NET 9
- C# 最新语法特性
- 面向对象设计原则

## 设计模式

- **组合模式** - 特定业务对象通过组合方式包含通用属性接口
- **策略模式** - 通过业务类型枚举确定使用的映射策略
- **工厂模式** - GetTyped() 方法根据业务类型创建相应的业务对象