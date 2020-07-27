using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreCQRSdemo.Domain;
using NetCoreCQRSdemo.Domain.Entities;
using System;

namespace NetCoreCQRSdemo.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cocktail>().ToTable(nameof(Cocktail));
            modelBuilder.Entity<Ingredient>().ToTable(nameof(Ingredient));
            modelBuilder.Entity<RecipeExcerpt>().ToTable(nameof(RecipeExcerpt));
            modelBuilder.Entity<AppEvent>().ToTable(nameof(AppEvent));

            modelBuilder.Entity<Cocktail>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            modelBuilder.Entity<Ingredient>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            modelBuilder.Entity<RecipeExcerpt>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            modelBuilder.Entity<AppEvent>(e =>
            {
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.Id).IsUnique();
                e.HasQueryFilter(e => e.EntityStatus == Domain.Enumerations.eEntityStatus.Active);
            });

            base.OnModelCreating(modelBuilder);
        }

        // DbSets
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeExcerpt> Excerpts { get; set; }
        public DbSet<AppEvent> Events { get; set; }
    }
}
