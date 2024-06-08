﻿// <auto-generated />
using System;
using Mango.Services.ProductAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ProductAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    partial class ApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-rc.1.23419.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ProductAPI.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = new Guid("d55dee90-1d73-48f1-b716-a5e5f9de04b7"),
                            CategoryName = "Appetizer",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://placehold.co/603x403",
                            Name = "Samosa",
                            Price = 15.0
                        },
                        new
                        {
                            ProductId = new Guid("3b221ab6-21df-48c8-a02d-0e0ade2169f3"),
                            CategoryName = "Appetizer",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://placehold.co/602x402",
                            Name = "Paneer Tikka",
                            Price = 13.99
                        },
                        new
                        {
                            ProductId = new Guid("5cfd8243-425f-460c-8dce-2419306f1122"),
                            CategoryName = "Dessert",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://placehold.co/601x401",
                            Name = "Sweet Pie",
                            Price = 10.99
                        },
                        new
                        {
                            ProductId = new Guid("5ebd2405-1a89-466f-b5c0-e7b81b055e1e"),
                            CategoryName = "Entree",
                            Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                            ImageUrl = "https://placehold.co/600x400",
                            Name = "Pav Bhaji",
                            Price = 15.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
