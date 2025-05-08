using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations.AccountDb
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1d3c1709-cff0-49a4-8d09-9f95aad66d4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5ecc550e-173a-400b-832c-518663268d2c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "693297ab-870e-4397-953f-168b9eeb3af6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c85daf18-3377-4362-bd84-39e021dca0a9");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f429fb5-a2b9-46d8-b796-525a8c812fe7", null, "Admin", "ADMIN" },
                    { "7ac6ff34-71ad-4b03-b5c4-40cb67838e09", null, "HR", "HR" },
                    { "e20d70cc-a813-485a-a510-4ba90d29dfce", null, "Employee", "EMPLOYEE" },
                    { "e4582fd2-3fa6-4b1f-b0f3-2fbe606fef73", null, "Payroll", "PAYROLL" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f429fb5-a2b9-46d8-b796-525a8c812fe7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ac6ff34-71ad-4b03-b5c4-40cb67838e09");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e20d70cc-a813-485a-a510-4ba90d29dfce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4582fd2-3fa6-4b1f-b0f3-2fbe606fef73");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1d3c1709-cff0-49a4-8d09-9f95aad66d4f", null, "HR", "HR" },
                    { "5ecc550e-173a-400b-832c-518663268d2c", null, "Employee", "EMPLOYEE" },
                    { "693297ab-870e-4397-953f-168b9eeb3af6", null, "Admin", "ADMIN" },
                    { "c85daf18-3377-4362-bd84-39e021dca0a9", null, "Payroll", "PAYROLL" }
                });
        }
    }
}
