# 灵活属性系统实现

## 概述

这个项目实现了一个灵活的属性系统，用于处理不同业务类型下的特定属性需求。以发票为例，不同业务类型（房屋租赁、建筑服务、货物运输服务等）的发票可能需要不同的特定属性，但我们通过通用属性字段和配置系统来避免数据库表结构的复杂化。

## 设计思路

### 核心概念

1. **通用属性字段**: 在主实体类（Invoice）中定义若干个通用的属性字段，包括：
   - `IntProperty1-3`: 整数类型属性
   - `StringProperty1-5`: 字符串类型属性
   - `DecimalProperty1-3`: 小数类型属性
   - `DateProperty1-2`: 日期类型属性
   - `BoolProperty1-2`: 布尔类型属性

2. **业务类型枚举**: 定义不同的业务类型来区分具体的业务场景

3. **配置系统**: 通过配置表定义每种业务类型下这些通用属性字段的具体含义

4. **类型化对象**: 实现 `GetTyped()` 方法，返回具有具体业务含义的特定业务对象

## 架构组件

### 1. 枚举定义

```csharp
// 业务类型
public enum BusinessType
{
    HouseRental,        // 房屋租赁
    ConstructionService, // 建筑服务
    TransportService    // 货物运输服务
}

// 属性类型
public enum PropertyType
{
    Int, String, Decimal, Date, Bool
}
```

### 2. 配置系统

- `PropertyConfiguration`: 定义单个属性的配置信息
- `BusinessTypeConfiguration`: 定义特定业务类型的所有属性配置
- `PropertyConfigurationManager`: 管理所有配置信息的静态类

### 3. 主实体类

`Invoice` 类包含：
- 基础发票信息（ID、发票号、业务类型、日期、金额）
- 通用属性字段
- `GetTyped()` 方法返回特定的业务对象

### 4. 特定业务对象

- `ITypedInvoice`: 特定业务对象的通用接口
- `HouseRentalInvoice`: 房屋租赁发票
- `ConstructionServiceInvoice`: 建筑服务发票
- `TransportServiceInvoice`: 货物运输服务发票

## 使用示例

### 创建房屋租赁发票

```csharp
var houseRentalInvoice = new Invoice
{
    Id = 1,
    InvoiceNumber = "INV-2024-001",
    BusinessType = BusinessType.HouseRental,
    IssueDate = DateTime.Now,
    Amount = 5000m,
    StringProperty1 = "北京市朝阳区望京街道1号", // 房产地址
    DecimalProperty1 = 120.5m,                   // 租赁面积
    DateProperty1 = new DateTime(2024, 1, 1),    // 租赁开始日期
    DateProperty2 = new DateTime(2024, 12, 31),  // 租赁结束日期
    BoolProperty1 = true,                        // 包含水电费
    IntProperty1 = 3,                            // 房间数量
    StringProperty2 = "张三",                     // 房东姓名
    StringProperty3 = "李四"                      // 租客姓名
};

// 获取特定业务对象
var typedHouseRental = houseRentalInvoice.GetTyped() as HouseRentalInvoice;
Console.WriteLine($"房产地址: {typedHouseRental.PropertyAddress}");
Console.WriteLine($"租赁面积: {typedHouseRental.RentalArea} 平方米");
```

### 属性配置查询

```csharp
// 获取业务类型配置
var config = PropertyConfigurationManager.GetConfiguration(BusinessType.HouseRental);

// 获取特定属性配置
var addressConfig = PropertyConfigurationManager.GetPropertyConfiguration(
    BusinessType.HouseRental, "StringProperty1");
Console.WriteLine($"StringProperty1 的含义: {addressConfig.DisplayName}");
```

## 优势

1. **数据库设计简单**: 避免为每种业务类型创建独立的字段
2. **灵活性高**: 可以通过配置系统轻松添加新的业务类型
3. **类型安全**: 通过强类型的特定业务对象提供编译时检查
4. **可维护性好**: 配置集中管理，易于维护和扩展
5. **向后兼容**: 新增业务类型不影响现有代码

## 扩展性

### 添加新业务类型

1. 在 `BusinessType` 枚举中添加新类型
2. 创建新的特定业务对象类
3. 在 `PropertyConfigurationManager` 中添加配置
4. 在 `Invoice.GetTyped()` 方法中添加相应的 case

### 添加新属性类型

如果需要更多属性字段，可以在 `Invoice` 类中添加更多通用属性，并相应更新配置系统。

## 注意事项

1. 属性字段数量有限，需要根据实际业务需求合理规划
2. 配置信息建议存储在数据库中，而不是硬编码
3. 考虑添加属性验证机制确保数据完整性
4. 可以考虑添加属性映射的缓存机制提高性能

## 技术栈

- .NET 9
- C# 13
- 使用了 nullable reference types 确保代码安全性
- 使用了 required 属性确保关键字段不为空