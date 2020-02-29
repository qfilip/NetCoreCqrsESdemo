﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetCoreCQRSdemo.Persistence.Context;

namespace NetCoreCQRSdemo.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200229225752_initial-migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("NetCoreCQRSdemo.Domain.Entities.AppEvent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Arguments")
                        .HasColumnType("TEXT");

                    b.Property<int>("CommandCode")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("AppEvent");
                });

            modelBuilder.Entity("NetCoreCQRSdemo.Domain.Entities.Cocktail", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Strength")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Cocktail");
                });

            modelBuilder.Entity("NetCoreCQRSdemo.Domain.Entities.EventCount", b =>
                {
                    b.Property<int>("CurrentCount")
                        .HasColumnType("INTEGER");

                    b.ToTable("EventCount");
                });

            modelBuilder.Entity("NetCoreCQRSdemo.Domain.Entities.Ingredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CocktailId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("UnitOfMeasure")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CocktailId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("NetCoreCQRSdemo.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("NetCoreCQRSdemo.Domain.Entities.Cocktail", "Cocktail")
                        .WithMany("Ingredients")
                        .HasForeignKey("CocktailId");
                });
#pragma warning restore 612, 618
        }
    }
}
