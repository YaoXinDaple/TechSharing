using AnotherImplementation;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("=== 发票业务对象映射演示 ===\n");

// 示例1：房屋租赁发票
Console.WriteLine("1. 房屋租赁发票示例：");
var houseRentalInvoice = new Invoice
{
    Id = 1,
    InvoiceNumber = "INV-2024-001",
    IssueDate = DateTime.Now,
    TotalAmount = 5000m,
    CustomerName = "张三",
    BusinessType = SpecificBusinessType.HouseRental
};

// 通过特定业务对象设置属性
if (houseRentalInvoice.GetTyped() is HouseRentalBusiness houseRental)
{
    houseRental.HouseAddress = "北京市朝阳区xxx小区1号楼101室";
    houseRental.RentalPeriod = "12个月";
    houseRental.LandlordName = "李四";
    houseRental.HouseArea = 80;
    houseRental.RoomCount = 2;
    houseRental.MonthlyRent = 5000m;
    houseRental.Deposit = 10000m;
    houseRental.RentalStartDate = DateTime.Now;
    houseRental.RentalEndDate = DateTime.Now.AddYears(1);
    houseRental.IncludesFurniture = true;
    houseRental.AllowsPets = false;
}

// 转换为 DTO
var houseRentalDto = InvoiceMapper.ToDto(houseRentalInvoice);
Console.WriteLine($"发票号: {houseRentalDto.InvoiceNumber}");
Console.WriteLine($"客户: {houseRentalDto.CustomerName}");
Console.WriteLine($"总金额: {houseRentalDto.TotalAmount}");
if (houseRentalDto.SpecificBusinessData is HouseRentalDto rentalData)
{
    Console.WriteLine($"房屋地址: {rentalData.HouseAddress}");
    Console.WriteLine($"月租金: {rentalData.MonthlyRent}");
    Console.WriteLine($"房屋面积: {rentalData.HouseArea}平方米");
    Console.WriteLine($"包含家具: {(rentalData.IncludesFurniture == true ? "是" : "否")}");
}
Console.WriteLine();

// 示例2：建筑服务发票
Console.WriteLine("2. 建筑服务发票示例：");
var constructionInvoice = new Invoice
{
    Id = 2,
    InvoiceNumber = "INV-2024-002",
    IssueDate = DateTime.Now,
    TotalAmount = 200000m,
    CustomerName = "建筑公司A",
    BusinessType = SpecificBusinessType.ConstructionService
};

// 通过特定业务对象设置属性
if (constructionInvoice.GetTyped() is ConstructionServiceBusiness construction)
{
    construction.ProjectAddress = "上海市浦东新区xxx路xxx号";
    construction.ServiceType = "装修工程";
    construction.ContractorName = "建筑承包商B";
    construction.ProjectManager = "王五";
    construction.ConstructionArea = 500;
    construction.EstimatedDuration = 60;
    construction.WorkerCount = 10;
    construction.MaterialCost = 100000m;
    construction.LaborCost = 80000m;
    construction.EquipmentCost = 20000m;
    construction.StartDate = DateTime.Now;
    construction.CompletionDate = DateTime.Now.AddDays(60);
    construction.RequiresPermit = true;
    construction.IsEmergencyProject = false;
}

// 转换为 DTO
var constructionDto = InvoiceMapper.ToDto(constructionInvoice);
Console.WriteLine($"发票号: {constructionDto.InvoiceNumber}");
Console.WriteLine($"客户: {constructionDto.CustomerName}");
Console.WriteLine($"总金额: {constructionDto.TotalAmount}");
if (constructionDto.SpecificBusinessData is ConstructionServiceDto constructionData)
{
    Console.WriteLine($"项目地址: {constructionData.ProjectAddress}");
    Console.WriteLine($"服务类型: {constructionData.ServiceType}");
    Console.WriteLine($"施工面积: {constructionData.ConstructionArea}平方米");
    Console.WriteLine($"材料费用: {constructionData.MaterialCost}");
    Console.WriteLine($"人工费用: {constructionData.LaborCost}");
}
Console.WriteLine();

// 示例3：货物运输服务发票
Console.WriteLine("3. 货物运输服务发票示例：");
var cargoInvoice = new Invoice
{
    Id = 3,
    InvoiceNumber = "INV-2024-003",
    IssueDate = DateTime.Now,
    TotalAmount = 8000m,
    CustomerName = "物流公司C",
    BusinessType = SpecificBusinessType.CargoTransportService
};

// 通过特定业务对象设置属性
if (cargoInvoice.GetTyped() is CargoTransportServiceBusiness cargo)
{
    cargo.OriginAddress = "广州市天河区xxx仓库";
    cargo.DestinationAddress = "深圳市南山区xxx公司";
    cargo.CargoType = "电子产品";
    cargo.TransportCompany = "快递公司D";
    cargo.DriverName = "赵六";
    cargo.CargoWeight = 1000;
    cargo.TransportDistance = 150;
    cargo.VehicleCount = 2;
    cargo.TransportFee = 6000m;
    cargo.FuelCost = 1500m;
    cargo.InsuranceFee = 500m;
    cargo.ShipmentDate = DateTime.Now;
    cargo.EstimatedArrivalDate = DateTime.Now.AddDays(1);
    cargo.RequiresRefrigeration = false;
    cargo.IsHazardousMaterial = false;
}

// 转换为 DTO
var cargoDto = InvoiceMapper.ToDto(cargoInvoice);
Console.WriteLine($"发票号: {cargoDto.InvoiceNumber}");
Console.WriteLine($"客户: {cargoDto.CustomerName}");
Console.WriteLine($"总金额: {cargoDto.TotalAmount}");
if (cargoDto.SpecificBusinessData is CargoTransportServiceDto cargoData)
{
    Console.WriteLine($"起始地址: {cargoData.OriginAddress}");
    Console.WriteLine($"目的地址: {cargoData.DestinationAddress}");
    Console.WriteLine($"货物类型: {cargoData.CargoType}");
    Console.WriteLine($"货物重量: {cargoData.CargoWeight}公斤");
    Console.WriteLine($"运输距离: {cargoData.TransportDistance}公里");
    Console.WriteLine($"运输费用: {cargoData.TransportFee}");
}

Console.WriteLine("\n=== 演示完成 ===");
