﻿// <auto-generated />
using System;
using Mango.Services.ShoppingCartAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Mango.Services.ShoppingCartAPI.Migrations
{
    [DbContext(typeof(ApiDbContext))]
    [Migration("20240711033621_addshoppingcatrtables")]
    partial class addshoppingcatrtables
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-rc.1.23419.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Models.CartDetail", b =>
                {
                    b.Property<Guid>("CartDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartHeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartDetailsId");

                    b.HasIndex("CartHeaderId");

                    b.ToTable("CartDetails");
                });

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Models.CartHeader", b =>
                {
                    b.Property<Guid>("CartHeaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CouponCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CartHeaderId");

                    b.ToTable("CartHeaders");
                });

            modelBuilder.Entity("Mango.Services.ShoppingCartAPI.Models.CartDetail", b =>
                {
                    b.HasOne("Mango.Services.ShoppingCartAPI.Models.CartHeader", "CartHeader")
                        .WithMany()
                        .HasForeignKey("CartHeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CartHeader");
                });
#pragma warning restore 612, 618
        }
    }
}
