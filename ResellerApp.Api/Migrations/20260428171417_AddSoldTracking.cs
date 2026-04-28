using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResellerApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddSoldTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateSold",
                table: "Items",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSold",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateSold",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "IsSold",
                table: "Items");
        }
    }
}
