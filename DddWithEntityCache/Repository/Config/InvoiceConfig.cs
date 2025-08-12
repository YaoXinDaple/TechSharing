using DddWithEntityCache.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddWithEntityCache.Repository.Config
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id).ValueGeneratedNever();
            builder.Property(i => i.CustomerName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.SellerName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Amount).HasColumnType("decimal(18,2)").IsRequired();
            // Configure the relationship with InvoiceEntry
            builder.HasMany(i => i.Entries)
                   .WithOne()
                   .HasForeignKey(e => e.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
