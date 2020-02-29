using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Entities;

namespace NetCoreCQRSdemo.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // base.Database.EnsureCreated();
            base.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>().ToTable(nameof(Cocktail));
            modelBuilder.Entity<Ingredient>().ToTable(nameof(Ingredient));
            modelBuilder.Entity<AppEvent>().ToTable(nameof(AppEvent));
            modelBuilder.Entity<EventCount>().ToTable(nameof(EventCount));

            modelBuilder.Entity<Cocktail>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
            });

            modelBuilder.Entity<Ingredient>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasOne(e => e.Cocktail);
            });

            modelBuilder.Entity<AppEvent>(e =>
            {
                e.HasKey(e => e.Id);
            });

            modelBuilder.Entity<EventCount>(e => e.HasNoKey());


            base.OnModelCreating(modelBuilder);
        }

        // DbSets
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<AppEvent> Events { get; set; }
        public DbSet<EventCount> EventCount { get; set; }
    }
}
