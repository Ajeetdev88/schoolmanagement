using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsOrderPlaceToCartItemsAndModifiedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderPlaced",
                table: "ShoppinCart");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderPlaced",
                table: "ShoppinCartItem",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "MOdifiedOn",
                table: "ShoppinCartItem",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "MOdifiedOn",
                table: "ShoppinCart",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderPlaced",
                table: "ShoppinCartItem");

            migrationBuilder.DropColumn(
                name: "MOdifiedOn",
                table: "ShoppinCartItem");

            migrationBuilder.DropColumn(
                name: "MOdifiedOn",
                table: "ShoppinCart");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderPlaced",
                table: "ShoppinCart",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
