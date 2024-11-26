using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTransactionIdAndDiscountToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartCartID",
                table: "ShoppinCartItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusStatusID",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders",
                column: "OrderStatusStatusID",
                principalTable: "OrderStatus",
                principalColumn: "StatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem",
                column: "ShoppingCartCartID",
                principalTable: "ShoppinCart",
                principalColumn: "CartID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "OrderItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartCartID",
                table: "ShoppinCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatusStatusID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderStatus_OrderStatusStatusID",
                table: "Orders",
                column: "OrderStatusStatusID",
                principalTable: "OrderStatus",
                principalColumn: "StatusID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem",
                column: "ShoppingCartCartID",
                principalTable: "ShoppinCart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
