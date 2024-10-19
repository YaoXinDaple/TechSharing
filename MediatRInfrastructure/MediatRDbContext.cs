using MediatRInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MediatRInfrastructure
{
    public class MediatRDbContext : DbContext
    {
        public MediatRDbContext(DbContextOptions<MediatRDbContext> options) : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasKey(c => c.Id);
            modelBuilder.Entity<ToDo>().Property(c => c.Title).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<ToDo>().Property(c => c.IsDone).IsRequired();
            modelBuilder.Entity<ToDo>().Property(c => c.Description).HasMaxLength(512).IsRequired();
            modelBuilder.Entity<ToDo>().Property(c => c.CreationTime).IsRequired();
            modelBuilder.Entity<ToDo>().Property(c => c.ComplationTime).IsRequired(false);


            base.OnModelCreating(modelBuilder);
        }
    }
}
