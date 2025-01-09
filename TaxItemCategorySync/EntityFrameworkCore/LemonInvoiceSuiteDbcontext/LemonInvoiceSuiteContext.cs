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

    public virtual DbSet<LemonTaxItemCategory> LemonTaxItemCategories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;database=LemonInvoiceSuite;Integrated Security=false;Uid=sa;Pwd=123456;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
