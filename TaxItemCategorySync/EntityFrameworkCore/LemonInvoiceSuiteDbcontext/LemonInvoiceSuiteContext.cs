using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TaxItemCategorySync.EntityFrameworkCore.LemonInvoiceSuiteDbcontext;

public partial class LemonInvoiceSuiteContext : DbContext
{
    public LemonInvoiceSuiteContext()
    {
    }

    public LemonInvoiceSuiteContext(DbContextOptions<LemonInvoiceSuiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ArchivedInvoice> LemonArchivedInvoices { get; set; }

    public virtual DbSet<LemonTaxItemCategory> LemonTaxItemCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;database=LemonInvoiceSuite;Integrated Security=false;Uid=sa;Pwd=123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ArchivedInvoice>(entity =>
        {
            entity.HasKey(e => e.Id).IsClustered(false);

            entity.HasIndex(e => new { e.CompanyId, e.DigitalInvoiceCode, e.InvoiceNumber }, "IX_LemonArchivedInvoices_CompanyId_DigitalInvoiceCode_InvoiceNumber");

            entity.HasIndex(e => new { e.CompanyId, e.IssueDate }, "IX_LemonArchivedInvoices_CompanyId_IssueDate").IsClustered();

            entity.HasIndex(e => e.EisInvoiceId, "IX_LemonArchivedInvoices_EisInvoiceId")
                .IsUnique()
                .HasFilter("([EisInvoiceId] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BlueInvoiceDigitalInvoiceCode).HasMaxLength(32);
            entity.Property(e => e.BuyerName)
                .HasMaxLength(256)
                .HasColumnName("Buyer_Name");
            entity.Property(e => e.BuyerUscic)
                .HasMaxLength(32)
                .HasColumnName("Buyer_Uscic");
            entity.Property(e => e.ConcurrencyStamp).HasMaxLength(40);
            entity.Property(e => e.DigitalInvoiceCode).HasMaxLength(32);
            entity.Property(e => e.InvoiceCode).HasMaxLength(32);
            entity.Property(e => e.InvoiceNumber).HasMaxLength(32);
            entity.Property(e => e.Issuer).HasMaxLength(32);
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(128)
                .HasColumnName("Recipient_Email");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(32)
                .HasColumnName("Recipient_Name");
            entity.Property(e => e.RecipientPhoneNumber)
                .HasMaxLength(32)
                .HasColumnName("Recipient_PhoneNumber");
            entity.Property(e => e.RedConfirmationOrderCode).HasMaxLength(32);
            entity.Property(e => e.Remark).HasMaxLength(2000);
            entity.Property(e => e.SellerName)
                .HasMaxLength(256)
                .HasColumnName("Seller_Name");
            entity.Property(e => e.SellerUscic)
                .HasMaxLength(32)
                .HasColumnName("Seller_Uscic");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmountWithTax).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalTaxAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<LemonTaxItemCategory>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.KeyWord).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.ParentTaxCode).HasMaxLength(19);
            entity.Property(e => e.RealTimeTaxRebateSign).HasMaxLength(64);
            entity.Property(e => e.ShortName).HasMaxLength(256);
            entity.Property(e => e.TaxCode).HasMaxLength(19);
            entity.Property(e => e.TaxRateTypes).HasMaxLength(32);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
