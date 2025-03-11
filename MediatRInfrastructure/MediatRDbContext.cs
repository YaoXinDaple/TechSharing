using MediatRInfrastructure.Models;
using MediatRInfrastructure.Plans;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace MediatRInfrastructure
{
    public class MediatRDbContext : DbContext
    {
        public MediatRDbContext(DbContextOptions<MediatRDbContext> options) : base(options)
        {
        }

        public DbSet<ToDo> ToDos { get; set; }

        public DbSet<Plan> Plans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine,LogLevel.Information);
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



            modelBuilder.Entity<Plan>().HasKey(x => x.Id);
            modelBuilder.Entity<Plan>().Property(x => x.Name).IsRequired().HasMaxLength(128);

            modelBuilder.Entity<Plan>().OwnsOne(x => x.Elapse, elapse =>
            {
                elapse.Property(x => x.Start).IsRequired();
                elapse.Property(x => x.End).IsRequired();
                elapse.Property(x => x.NumberOfDays).IsRequired(false);
            });

            modelBuilder.Entity<Plan>().Navigation(x => x.Elapse).IsRequired(false);

            modelBuilder.Entity<Plan>().Property(x => x.CompleteTime).IsRequired(false);
            modelBuilder.Entity<Plan>().Property(x => x.CreateUser).IsRequired().HasMaxLength(64);
            modelBuilder.Entity<Plan>().Property(x => x.CreationTime).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }

    public class MediatRDbContextFactory : IDesignTimeDbContextFactory<MediatRDbContext>
    {
        public MediatRDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MediatRDbContext>();
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ToDoTestDb;Persist Security Info=True;User ID=sa;Password=123456;encrypt=false");

            return new MediatRDbContext(optionsBuilder.Options);
        }
    }
}
