using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForOrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderStatusStatusID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.StatusID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusStatusID",
                table: "Orders",
                column: "OrderStatusStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders",
                column: "OrderStatusStatusID",
                principalTable: "OrderStatus",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderStatusStatusID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderStatusStatusID",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusID",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
