using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inventoryinventorylogsmakenull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantVariantID",
                table: "Inventory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory",
                column: "ProductVariantVariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVariantVariantID",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory",
                column: "ProductVariantVariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
