using System;
using System.Collections.Generic;

namespace TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

public partial class LemonTaxItemCategory
{
    public LemonTaxItemCategory(int id, string name, string taxCode)
    {
        Id = id;
        Name = name;
        TaxCode = taxCode;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string TaxCode { get; set; } = null!;

    public string? ShortName { get; set; }

    public string? Description { get; set; }


    public int ParentId { get; set; }

    public string? ParentTaxCode { get; set; }

    public bool IsEndLevel { get; set; }

    public bool? AvailableForDifferentialTaxation { get; set; }

    public string? RealTimeTaxRebateSign { get; set; }

    public string? TaxRateTypes { get; set; }

    public string? KeyWord { get; set; }
}
