using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OutboxExample.Contracts.Persistence.Mappings;
using OutboxExample.Contracts.Persistence.Models;

namespace OutboxExample.Contracts.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<StandardEntry> InvoiceEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservationMapping());
            modelBuilder.ApplyConfiguration(new InvoiceMapping());
            modelBuilder.ApplyConfiguration(new StandardEntryMapping());
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();
            modelBuilder.AddInboxStateEntity();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
