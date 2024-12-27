using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutboxExample.Contracts.Persistence.Models;

namespace OutboxExample.Contracts.Persistence.Mappings
{
    public class InvoiceMapping : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable(nameof(Invoice));
            builder.Property(x => x.OrderNo).HasMaxLength(128);
            builder.Property(x => x.BuyerName).HasMaxLength(128);

            builder.HasMany(x => x.Entries).WithOne().HasForeignKey("InvoiceId").OnDelete(DeleteBehavior.Cascade);
        }
    }


    public class StandardEntryMapping : IEntityTypeConfiguration<StandardEntry>
    {
        public void Configure(EntityTypeBuilder<StandardEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.ToTable(nameof(StandardEntry));
            builder.Property(x => x.Number);
            builder.Property(x => x.ItemName).HasMaxLength(128);
            builder.Property(x => x.Amount).HasMaxLength(128);
            builder.Property(x => x.IsDiscountLine);
        }
    }
}
