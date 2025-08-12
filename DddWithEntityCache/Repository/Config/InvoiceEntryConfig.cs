using DddWithEntityCache.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DddWithEntityCache.Repository.Config
{
    public class InvoiceEntryConfig : IEntityTypeConfiguration<InvoiceEntry>
    {
        public void Configure(EntityTypeBuilder<InvoiceEntry> builder)
        {
            builder.ToTable("InvoiceEntries");
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.InvoiceId).IsRequired();
            builder.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(e => e.Quantity).IsRequired();
            
            // Configure the relationship with Invoice
            builder.HasOne<Invoice>()
                .WithMany(i => i.Entries)
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
