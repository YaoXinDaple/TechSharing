using System;
using System.Collections.Generic;

// 业务类型枚举
public enum BusinessType
{
    HouseRental,        // 房屋租赁
    ConstructionService, // 建筑服务
    TransportService    // 货物运输服务
}

// 属性类型枚举
public enum PropertyType
{
    Int,
    String,
    Decimal,
    Date,
    Bool
}

// 属性配置类
public class PropertyConfiguration
{
    public required string PropertyName { get; set; }
    public PropertyType PropertyType { get; set; }
    public required string DisplayName { get; set; }
    public required string Description { get; set; }
}

// 业务类型配置
public class BusinessTypeConfiguration
{
    public BusinessType BusinessType { get; set; }
    public Dictionary<string, PropertyConfiguration> PropertyConfigurations { get; set; } = new();
}

// 主实体类 - 发票
public class Invoice
{
    public int Id { get; set; }
    public required string InvoiceNumber { get; set; }
    public BusinessType BusinessType { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal Amount { get; set; }

    // 通用属性字段
    public int? IntProperty1 { get; set; }
    public int? IntProperty2 { get; set; }
    public int? IntProperty3 { get; set; }
    
    public string? StringProperty1 { get; set; }
    public string? StringProperty2 { get; set; }
    public string? StringProperty3 { get; set; }
    public string? StringProperty4 { get; set; }
    public string? StringProperty5 { get; set; }
    
    public decimal? DecimalProperty1 { get; set; }
    public decimal? DecimalProperty2 { get; set; }
    public decimal? DecimalProperty3 { get; set; }
    
    public DateTime? DateProperty1 { get; set; }
    public DateTime? DateProperty2 { get; set; }
    
    public bool? BoolProperty1 { get; set; }
    public bool? BoolProperty2 { get; set; }

    // 获取特定业务类型对象
    public ITypedInvoice GetTyped()
    {
        return BusinessType switch
        {
            BusinessType.HouseRental => new HouseRentalInvoice(this),
            BusinessType.ConstructionService => new ConstructionServiceInvoice(this),
            BusinessType.TransportService => new TransportServiceInvoice(this),
            _ => throw new NotSupportedException($"Business type {BusinessType} is not supported")
        };
    }
}

// 特定业务对象接口
public interface ITypedInvoice
{
    Invoice BaseInvoice { get; }
    BusinessType BusinessType { get; }
}

// 房屋租赁发票
public class HouseRentalInvoice : ITypedInvoice
{
    private readonly Invoice _baseInvoice;

    public HouseRentalInvoice(Invoice baseInvoice)
    {
        _baseInvoice = baseInvoice;
    }

    public Invoice BaseInvoice => _baseInvoice;
    public BusinessType BusinessType => BusinessType.HouseRental;

    // 房屋租赁特定属性
    public string? PropertyAddress => _baseInvoice.StringProperty1;
    public decimal RentalArea => _baseInvoice.DecimalProperty1 ?? 0;
    public DateTime RentalStartDate => _baseInvoice.DateProperty1 ?? DateTime.MinValue;
    public DateTime RentalEndDate => _baseInvoice.DateProperty2 ?? DateTime.MinValue;
    public bool IncludesUtilities => _baseInvoice.BoolProperty1 ?? false;
    public int RoomCount => _baseInvoice.IntProperty1 ?? 0;
    public string? LandlordName => _baseInvoice.StringProperty2;
    public string? TenantName => _baseInvoice.StringProperty3;
}

// 建筑服务发票
public class ConstructionServiceInvoice : ITypedInvoice
{
    private readonly Invoice _baseInvoice;

    public ConstructionServiceInvoice(Invoice baseInvoice)
    {
        _baseInvoice = baseInvoice;
    }

    public Invoice BaseInvoice => _baseInvoice;
    public BusinessType BusinessType => BusinessType.ConstructionService;

    // 建筑服务特定属性
    public string? ProjectName => _baseInvoice.StringProperty1;
    public string? ProjectLocation => _baseInvoice.StringProperty2;
    public decimal ConstructionArea => _baseInvoice.DecimalProperty1 ?? 0;
    public DateTime ProjectStartDate => _baseInvoice.DateProperty1 ?? DateTime.MinValue;
    public DateTime ProjectEndDate => _baseInvoice.DateProperty2 ?? DateTime.MinValue;
    public string? ContractorName => _baseInvoice.StringProperty3;
    public string? ProjectType => _baseInvoice.StringProperty4;
    public int WorkerCount => _baseInvoice.IntProperty1 ?? 0;
    public bool RequiresPermit => _baseInvoice.BoolProperty1 ?? false;
    public decimal MaterialCost => _baseInvoice.DecimalProperty2 ?? 0;
}

// 货物运输服务发票
public class TransportServiceInvoice : ITypedInvoice
{
    private readonly Invoice _baseInvoice;

    public TransportServiceInvoice(Invoice baseInvoice)
    {
        _baseInvoice = baseInvoice;
    }

    public Invoice BaseInvoice => _baseInvoice;
    public BusinessType BusinessType => BusinessType.TransportService;

    // 货物运输服务特定属性
    public string? OriginAddress => _baseInvoice.StringProperty1;
    public string? DestinationAddress => _baseInvoice.StringProperty2;
    public decimal CargoWeight => _baseInvoice.DecimalProperty1 ?? 0;
    public decimal Distance => _baseInvoice.DecimalProperty2 ?? 0;
    public string? VehicleType => _baseInvoice.StringProperty3;
    public string? DriverName => _baseInvoice.StringProperty4;
    public DateTime PickupDate => _baseInvoice.DateProperty1 ?? DateTime.MinValue;
    public DateTime DeliveryDate => _baseInvoice.DateProperty2 ?? DateTime.MinValue;
    public bool IsFragile => _baseInvoice.BoolProperty1 ?? false;
    public bool RequiresRefrigeration => _baseInvoice.BoolProperty2 ?? false;
    public int PackageCount => _baseInvoice.IntProperty1 ?? 0;
}

// 配置管理器
public static class PropertyConfigurationManager
{
    private static readonly Dictionary<BusinessType, BusinessTypeConfiguration> _configurations = new();

    static PropertyConfigurationManager()
    {
        InitializeConfigurations();
    }

    private static void InitializeConfigurations()
    {
        // 房屋租赁配置
        var houseRentalConfig = new BusinessTypeConfiguration
        {
            BusinessType = BusinessType.HouseRental,
            PropertyConfigurations = new Dictionary<string, PropertyConfiguration>
            {
                ["StringProperty1"] = new() { PropertyName = "PropertyAddress", PropertyType = PropertyType.String, DisplayName = "房产地址", Description = "租赁房产的详细地址" },
                ["DecimalProperty1"] = new() { PropertyName = "RentalArea", PropertyType = PropertyType.Decimal, DisplayName = "租赁面积", Description = "租赁房产的面积（平方米）" },
                ["DateProperty1"] = new() { PropertyName = "RentalStartDate", PropertyType = PropertyType.Date, DisplayName = "租赁开始日期", Description = "租赁合同开始日期" },
                ["DateProperty2"] = new() { PropertyName = "RentalEndDate", PropertyType = PropertyType.Date, DisplayName = "租赁结束日期", Description = "租赁合同结束日期" },
                ["BoolProperty1"] = new() { PropertyName = "IncludesUtilities", PropertyType = PropertyType.Bool, DisplayName = "包含水电费", Description = "租金是否包含水电费" },
                ["IntProperty1"] = new() { PropertyName = "RoomCount", PropertyType = PropertyType.Int, DisplayName = "房间数量", Description = "租赁房产的房间数量" },
                ["StringProperty2"] = new() { PropertyName = "LandlordName", PropertyType = PropertyType.String, DisplayName = "房东姓名", Description = "房东的姓名" },
                ["StringProperty3"] = new() { PropertyName = "TenantName", PropertyType = PropertyType.String, DisplayName = "租客姓名", Description = "租客的姓名" }
            }
        };

        // 建筑服务配置
        var constructionConfig = new BusinessTypeConfiguration
        {
            BusinessType = BusinessType.ConstructionService,
            PropertyConfigurations = new Dictionary<string, PropertyConfiguration>
            {
                ["StringProperty1"] = new() { PropertyName = "ProjectName", PropertyType = PropertyType.String, DisplayName = "项目名称", Description = "建筑项目的名称" },
                ["StringProperty2"] = new() { PropertyName = "ProjectLocation", PropertyType = PropertyType.String, DisplayName = "项目地点", Description = "建筑项目的地点" },
                ["DecimalProperty1"] = new() { PropertyName = "ConstructionArea", PropertyType = PropertyType.Decimal, DisplayName = "建筑面积", Description = "建筑项目的面积（平方米）" },
                ["DateProperty1"] = new() { PropertyName = "ProjectStartDate", PropertyType = PropertyType.Date, DisplayName = "项目开始日期", Description = "建筑项目开始日期" },
                ["DateProperty2"] = new() { PropertyName = "ProjectEndDate", PropertyType = PropertyType.Date, DisplayName = "项目结束日期", Description = "建筑项目结束日期" },
                ["StringProperty3"] = new() { PropertyName = "ContractorName", PropertyType = PropertyType.String, DisplayName = "承包商名称", Description = "负责建筑项目的承包商" },
                ["StringProperty4"] = new() { PropertyName = "ProjectType", PropertyType = PropertyType.String, DisplayName = "项目类型", Description = "建筑项目的类型（如住宅、商业等）" },
                ["IntProperty1"] = new() { PropertyName = "WorkerCount", PropertyType = PropertyType.Int, DisplayName = "工人数量", Description = "项目中的工人数量" },
                ["BoolProperty1"] = new() { PropertyName = "RequiresPermit", PropertyType = PropertyType.Bool, DisplayName = "需要许可证", Description = "项目是否需要建筑许可证" },
                ["DecimalProperty2"] = new() { PropertyName = "MaterialCost", PropertyType = PropertyType.Decimal, DisplayName = "材料成本", Description = "项目材料的成本" }
            }
        };

        // 货物运输服务配置
        var transportConfig = new BusinessTypeConfiguration
        {
            BusinessType = BusinessType.TransportService,
            PropertyConfigurations = new Dictionary<string, PropertyConfiguration>
            {
                ["StringProperty1"] = new() { PropertyName = "OriginAddress", PropertyType = PropertyType.String, DisplayName = "起始地址", Description = "货物运输的起始地址" },
                ["StringProperty2"] = new() { PropertyName = "DestinationAddress", PropertyType = PropertyType.String, DisplayName = "目的地址", Description = "货物运输的目的地址" },
                ["DecimalProperty1"] = new() { PropertyName = "CargoWeight", PropertyType = PropertyType.Decimal, DisplayName = "货物重量", Description = "运输货物的重量（公斤）" },
                ["DecimalProperty2"] = new() { PropertyName = "Distance", PropertyType = PropertyType.Decimal, DisplayName = "运输距离", Description = "运输距离（公里）" },
                ["StringProperty3"] = new() { PropertyName = "VehicleType", PropertyType = PropertyType.String, DisplayName = "车辆类型", Description = "运输车辆的类型" },
                ["StringProperty4"] = new() { PropertyName = "DriverName", PropertyType = PropertyType.String, DisplayName = "司机姓名", Description = "运输司机的姓名" },
                ["DateProperty1"] = new() { PropertyName = "PickupDate", PropertyType = PropertyType.Date, DisplayName = "取货日期", Description = "货物取货日期" },
                ["DateProperty2"] = new() { PropertyName = "DeliveryDate", PropertyType = PropertyType.Date, DisplayName = "送达日期", Description = "货物送达日期" },
                ["BoolProperty1"] = new() { PropertyName = "IsFragile", PropertyType = PropertyType.Bool, DisplayName = "易碎品", Description = "货物是否为易碎品" },
                ["BoolProperty2"] = new() { PropertyName = "RequiresRefrigeration", PropertyType = PropertyType.Bool, DisplayName = "需要冷藏", Description = "货物是否需要冷藏" },
                ["IntProperty1"] = new() { PropertyName = "PackageCount", PropertyType = PropertyType.Int, DisplayName = "包裹数量", Description = "运输包裹的数量" }
            }
        };

        _configurations[BusinessType.HouseRental] = houseRentalConfig;
        _configurations[BusinessType.ConstructionService] = constructionConfig;
        _configurations[BusinessType.TransportService] = transportConfig;
    }

    public static BusinessTypeConfiguration? GetConfiguration(BusinessType businessType)
    {
        return _configurations.TryGetValue(businessType, out var config) ? config : null;
    }

    public static PropertyConfiguration? GetPropertyConfiguration(BusinessType businessType, string propertyName)
    {
        var config = GetConfiguration(businessType);
        return config?.PropertyConfigurations.TryGetValue(propertyName, out var propConfig) == true ? propConfig : null;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== 灵活属性系统演示 ===");
        
        // 演示房屋租赁发票
        Console.WriteLine("\n1. 房屋租赁发票示例:");
        var houseRentalInvoice = new Invoice
        {
            Id = 1,
            InvoiceNumber = "INV-2024-001",
            BusinessType = BusinessType.HouseRental,
            IssueDate = DateTime.Now,
            Amount = 5000m,
            StringProperty1 = "北京市朝阳区望京街道1号", // PropertyAddress
            DecimalProperty1 = 120.5m,                    // RentalArea
            DateProperty1 = new DateTime(2024, 1, 1),     // RentalStartDate
            DateProperty2 = new DateTime(2024, 12, 31),   // RentalEndDate
            BoolProperty1 = true,                         // IncludesUtilities
            IntProperty1 = 3,                             // RoomCount
            StringProperty2 = "张三",                      // LandlordName
            StringProperty3 = "李四"                       // TenantName
        };

        var typedHouseRental = houseRentalInvoice.GetTyped() as HouseRentalInvoice;
        if (typedHouseRental != null)
        {
            Console.WriteLine($"发票号: {typedHouseRental.BaseInvoice.InvoiceNumber}");
            Console.WriteLine($"房产地址: {typedHouseRental.PropertyAddress}");
            Console.WriteLine($"租赁面积: {typedHouseRental.RentalArea} 平方米");
            Console.WriteLine($"房间数量: {typedHouseRental.RoomCount}");
            Console.WriteLine($"房东: {typedHouseRental.LandlordName}");
            Console.WriteLine($"租客: {typedHouseRental.TenantName}");
            Console.WriteLine($"包含水电费: {(typedHouseRental.IncludesUtilities ? "是" : "否")}");
        }

        // 演示建筑服务发票
        Console.WriteLine("\n2. 建筑服务发票示例:");
        var constructionInvoice = new Invoice
        {
            Id = 2,
            InvoiceNumber = "INV-2024-002",
            BusinessType = BusinessType.ConstructionService,
            IssueDate = DateTime.Now,
            Amount = 500000m,
            StringProperty1 = "望京SOHO建设项目",           // ProjectName
            StringProperty2 = "北京市朝阳区望京",             // ProjectLocation
            DecimalProperty1 = 5000m,                     // ConstructionArea
            DateProperty1 = new DateTime(2024, 3, 1),     // ProjectStartDate
            DateProperty2 = new DateTime(2024, 12, 31),   // ProjectEndDate
            StringProperty3 = "北京建筑公司",               // ContractorName
            StringProperty4 = "商业建筑",                   // ProjectType
            IntProperty1 = 50,                            // WorkerCount
            BoolProperty1 = true,                         // RequiresPermit
            DecimalProperty2 = 300000m                    // MaterialCost
        };

        var typedConstruction = constructionInvoice.GetTyped() as ConstructionServiceInvoice;
        if (typedConstruction != null)
        {
            Console.WriteLine($"发票号: {typedConstruction.BaseInvoice.InvoiceNumber}");
            Console.WriteLine($"项目名称: {typedConstruction.ProjectName}");
            Console.WriteLine($"项目地点: {typedConstruction.ProjectLocation}");
            Console.WriteLine($"建筑面积: {typedConstruction.ConstructionArea} 平方米");
            Console.WriteLine($"承包商: {typedConstruction.ContractorName}");
            Console.WriteLine($"项目类型: {typedConstruction.ProjectType}");
            Console.WriteLine($"工人数量: {typedConstruction.WorkerCount}");
            Console.WriteLine($"材料成本: {typedConstruction.MaterialCost:C}");
        }

        // 演示货物运输服务发票
        Console.WriteLine("\n3. 货物运输服务发票示例:");
        var transportInvoice = new Invoice
        {
            Id = 3,
            InvoiceNumber = "INV-2024-003",
            BusinessType = BusinessType.TransportService,
            IssueDate = DateTime.Now,
            Amount = 2000m,
            StringProperty1 = "北京市海淀区中关村",          // OriginAddress
            StringProperty2 = "上海市浦东新区陆家嘴",        // DestinationAddress
            DecimalProperty1 = 1500m,                     // CargoWeight
            DecimalProperty2 = 1200m,                     // Distance
            StringProperty3 = "大型货车",                   // VehicleType
            StringProperty4 = "王五",                       // DriverName
            DateProperty1 = new DateTime(2024, 2, 15),    // PickupDate
            DateProperty2 = new DateTime(2024, 2, 17),    // DeliveryDate
            BoolProperty1 = false,                        // IsFragile
            BoolProperty2 = true,                         // RequiresRefrigeration
            IntProperty1 = 20                             // PackageCount
        };

        var typedTransport = transportInvoice.GetTyped() as TransportServiceInvoice;
        if (typedTransport != null)
        {
            Console.WriteLine($"发票号: {typedTransport.BaseInvoice.InvoiceNumber}");
            Console.WriteLine($"起始地址: {typedTransport.OriginAddress}");
            Console.WriteLine($"目的地址: {typedTransport.DestinationAddress}");
            Console.WriteLine($"货物重量: {typedTransport.CargoWeight} 公斤");
            Console.WriteLine($"运输距离: {typedTransport.Distance} 公里");
            Console.WriteLine($"车辆类型: {typedTransport.VehicleType}");
            Console.WriteLine($"司机: {typedTransport.DriverName}");
            Console.WriteLine($"包裹数量: {typedTransport.PackageCount}");
            Console.WriteLine($"需要冷藏: {(typedTransport.RequiresRefrigeration ? "是" : "否")}");
        }

        // 演示配置系统
        Console.WriteLine("\n4. 配置系统演示:");
        var houseRentalConfig = PropertyConfigurationManager.GetConfiguration(BusinessType.HouseRental);
        if (houseRentalConfig != null)
        {
            Console.WriteLine($"房屋租赁业务类型配置的属性数量: {houseRentalConfig.PropertyConfigurations.Count}");
            
            var addressConfig = PropertyConfigurationManager.GetPropertyConfiguration(BusinessType.HouseRental, "StringProperty1");
            if (addressConfig != null)
            {
                Console.WriteLine($"StringProperty1 在房屋租赁中的含义: {addressConfig.DisplayName} - {addressConfig.Description}");
            }
        }

        Console.WriteLine("\n=== 演示完成 ===");
    }
}
