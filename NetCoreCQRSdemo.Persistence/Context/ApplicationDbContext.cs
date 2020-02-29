using Microsoft.EntityFrameworkCore;
using NetCoreCQRSdemo.Domain.Entities;

namespace NetCoreCQRSdemo.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>().ToTable(nameof(Cocktail));
            modelBuilder.Entity<Ingredient>().ToTable(nameof(Ingredient));

            modelBuilder.Entity<Cocktail>(e => {
                e.HasIndex(e => e.Id).IsUnique();
            });

            modelBuilder.Entity<Ingredient>(e => {
                e.HasIndex(e => e.Id).IsUnique();
                e.HasOne(e => e.Cocktail);
            });

            base.OnModelCreating(modelBuilder);
        }

        // DbSets
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
