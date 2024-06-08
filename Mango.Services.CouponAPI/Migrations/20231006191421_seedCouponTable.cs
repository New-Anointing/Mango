using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mango.Services.CouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedCouponTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountedAmount", "MinAmount" },
                values: new object[,]
                {
                    { new Guid("404e6d5c-734e-46d0-ba5a-98108da049a6"), "10OFF", 10.0, 20 },
                    { new Guid("5f9a1d50-365f-42a9-b2af-08ef2f32e3f7"), "20OFF", 20.0, 40 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: new Guid("404e6d5c-734e-46d0-ba5a-98108da049a6"));

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: new Guid("5f9a1d50-365f-42a9-b2af-08ef2f32e3f7"));
        }
    }
}
