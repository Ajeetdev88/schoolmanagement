using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInShoppingCartForRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingCartCartID",
                table: "ShoppinCartItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem",
                column: "ShoppingCartCartID",
                principalTable: "ShoppinCart",
                principalColumn: "CartID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                table: "ShoppinCartItem",
                column: "ShoppingCartCartID",
                principalTable: "ShoppinCart",
                principalColumn: "CartID");
        }
    }
}
