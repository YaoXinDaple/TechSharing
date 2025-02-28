using System;
using System.Collections.Generic;

namespace TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

public partial class ArchivedInvoice
{
    public Guid Id { get; set; }

    public Guid CompanyId { get; set; }

    public DateTime? ValidateTime { get; set; }

    public string InvoiceCode { get; set; } = null!;

    public string InvoiceNumber { get; set; } = null!;

    public string DigitalInvoiceCode { get; set; } = null!;

    public string BuyerName { get; set; } = null!;

    public string BuyerUscic { get; set; } = null!;

    public string SellerName { get; set; } = null!;

    public string SellerUscic { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal TotalTaxAmount { get; set; }

    public decimal TotalAmountWithTax { get; set; }

    public int Source { get; set; }

    public int InvoiceType { get; set; }

    public int State { get; set; }

    public int RiskState { get; set; }

    public string Issuer { get; set; } = null!;

    public string Remark { get; set; } = null!;

    public int Direction { get; set; }

    public bool IsAmountPositive { get; set; }

    public Guid? EisInvoiceId { get; set; }

    public string ExtraProperties { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public Guid? CreatorId { get; set; }

    public string? BlueInvoiceDigitalInvoiceCode { get; set; }

    public string? RedConfirmationOrderCode { get; set; }

    public string? RecipientEmail { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientPhoneNumber { get; set; }

    public int? SpecialServiceType { get; set; }
}
