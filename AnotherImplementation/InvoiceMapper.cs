namespace AnotherImplementation
{
    /// <summary>
    /// 发票映射器，负责 Invoice 与 InvoiceDto 之间的转换
    /// </summary>
    public static class InvoiceMapper
    {
        /// <summary>
        /// 将 Invoice 转换为 InvoiceDto
        /// </summary>
        /// <param name="invoice">发票实体</param>
        /// <returns>发票 DTO</returns>
        public static InvoiceDto ToDto(Invoice invoice)
        {
            var dto = new InvoiceDto
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                IssueDate = invoice.IssueDate,
                TotalAmount = invoice.TotalAmount,
                CustomerName = invoice.CustomerName,
                BusinessType = invoice.BusinessType
            };

            // 根据业务类型映射特定业务数据
            dto.SpecificBusinessData = invoice.BusinessType switch
            {
                SpecificBusinessType.HouseRental => MapToHouseRentalDto(invoice),
                SpecificBusinessType.ConstructionService => MapToConstructionServiceDto(invoice),
                SpecificBusinessType.CargoTransportService => MapToCargoTransportServiceDto(invoice),
                _ => null
            };

            return dto;
        }

        /// <summary>
        /// 将 InvoiceDto 转换为 Invoice
        /// </summary>
        /// <param name="dto">发票 DTO</param>
        /// <returns>发票实体</returns>
        public static Invoice ToEntity(InvoiceDto dto)
        {
            var invoice = new Invoice
            {
                Id = dto.Id,
                InvoiceNumber = dto.InvoiceNumber,
                IssueDate = dto.IssueDate,
                TotalAmount = dto.TotalAmount,
                CustomerName = dto.CustomerName,
                BusinessType = dto.BusinessType
            };

            // 根据业务类型映射特定业务数据
            switch (dto.BusinessType)
            {
                case SpecificBusinessType.HouseRental:
                    if (dto.SpecificBusinessData is HouseRentalDto houseRentalDto)
                        MapFromHouseRentalDto(invoice, houseRentalDto);
                    break;
                case SpecificBusinessType.ConstructionService:
                    if (dto.SpecificBusinessData is ConstructionServiceDto constructionDto)
                        MapFromConstructionServiceDto(invoice, constructionDto);
                    break;
                case SpecificBusinessType.CargoTransportService:
                    if (dto.SpecificBusinessData is CargoTransportServiceDto cargoDto)
                        MapFromCargoTransportServiceDto(invoice, cargoDto);
                    break;
            }

            return invoice;
        }

        private static HouseRentalDto MapToHouseRentalDto(Invoice invoice)
        {
            return new HouseRentalDto
            {
                HouseAddress = invoice.Property1,
                RentalPeriod = invoice.Property2,
                LandlordName = invoice.Property3,
                HouseArea = invoice.IntProperty1,
                RoomCount = invoice.IntProperty2,
                MonthlyRent = invoice.DecimalProperty1,
                Deposit = invoice.DecimalProperty2,
                RentalStartDate = invoice.DateProperty1,
                RentalEndDate = invoice.DateProperty2,
                IncludesFurniture = invoice.BoolProperty1,
                AllowsPets = invoice.BoolProperty2
            };
        }

        private static ConstructionServiceDto MapToConstructionServiceDto(Invoice invoice)
        {
            return new ConstructionServiceDto
            {
                ProjectAddress = invoice.Property1,
                ServiceType = invoice.Property2,
                ContractorName = invoice.Property3,
                ProjectManager = invoice.Property4,
                ConstructionArea = invoice.IntProperty1,
                EstimatedDuration = invoice.IntProperty2,
                WorkerCount = invoice.IntProperty3,
                MaterialCost = invoice.DecimalProperty1,
                LaborCost = invoice.DecimalProperty2,
                EquipmentCost = invoice.DecimalProperty3,
                StartDate = invoice.DateProperty1,
                CompletionDate = invoice.DateProperty2,
                RequiresPermit = invoice.BoolProperty1,
                IsEmergencyProject = invoice.BoolProperty2
            };
        }

        private static CargoTransportServiceDto MapToCargoTransportServiceDto(Invoice invoice)
        {
            return new CargoTransportServiceDto
            {
                OriginAddress = invoice.Property1,
                DestinationAddress = invoice.Property2,
                CargoType = invoice.Property3,
                TransportCompany = invoice.Property4,
                DriverName = invoice.Property5,
                CargoWeight = invoice.IntProperty1,
                TransportDistance = invoice.IntProperty2,
                VehicleCount = invoice.IntProperty3,
                TransportFee = invoice.DecimalProperty1,
                FuelCost = invoice.DecimalProperty2,
                InsuranceFee = invoice.DecimalProperty3,
                ShipmentDate = invoice.DateProperty1,
                EstimatedArrivalDate = invoice.DateProperty2,
                RequiresRefrigeration = invoice.BoolProperty1,
                IsHazardousMaterial = invoice.BoolProperty2
            };
        }

        private static void MapFromHouseRentalDto(Invoice invoice, HouseRentalDto dto)
        {
            invoice.Property1 = dto.HouseAddress;
            invoice.Property2 = dto.RentalPeriod;
            invoice.Property3 = dto.LandlordName;
            invoice.IntProperty1 = dto.HouseArea;
            invoice.IntProperty2 = dto.RoomCount;
            invoice.DecimalProperty1 = dto.MonthlyRent;
            invoice.DecimalProperty2 = dto.Deposit;
            invoice.DateProperty1 = dto.RentalStartDate;
            invoice.DateProperty2 = dto.RentalEndDate;
            invoice.BoolProperty1 = dto.IncludesFurniture;
            invoice.BoolProperty2 = dto.AllowsPets;
        }

        private static void MapFromConstructionServiceDto(Invoice invoice, ConstructionServiceDto dto)
        {
            invoice.Property1 = dto.ProjectAddress;
            invoice.Property2 = dto.ServiceType;
            invoice.Property3 = dto.ContractorName;
            invoice.Property4 = dto.ProjectManager;
            invoice.IntProperty1 = dto.ConstructionArea;
            invoice.IntProperty2 = dto.EstimatedDuration;
            invoice.IntProperty3 = dto.WorkerCount;
            invoice.DecimalProperty1 = dto.MaterialCost;
            invoice.DecimalProperty2 = dto.LaborCost;
            invoice.DecimalProperty3 = dto.EquipmentCost;
            invoice.DateProperty1 = dto.StartDate;
            invoice.DateProperty2 = dto.CompletionDate;
            invoice.BoolProperty1 = dto.RequiresPermit;
            invoice.BoolProperty2 = dto.IsEmergencyProject;
        }

        private static void MapFromCargoTransportServiceDto(Invoice invoice, CargoTransportServiceDto dto)
        {
            invoice.Property1 = dto.OriginAddress;
            invoice.Property2 = dto.DestinationAddress;
            invoice.Property3 = dto.CargoType;
            invoice.Property4 = dto.TransportCompany;
            invoice.Property5 = dto.DriverName;
            invoice.IntProperty1 = dto.CargoWeight;
            invoice.IntProperty2 = dto.TransportDistance;
            invoice.IntProperty3 = dto.VehicleCount;
            invoice.DecimalProperty1 = dto.TransportFee;
            invoice.DecimalProperty2 = dto.FuelCost;
            invoice.DecimalProperty3 = dto.InsuranceFee;
            invoice.DateProperty1 = dto.ShipmentDate;
            invoice.DateProperty2 = dto.EstimatedArrivalDate;
            invoice.BoolProperty1 = dto.RequiresRefrigeration;
            invoice.BoolProperty2 = dto.IsHazardousMaterial;
        }
    }
}