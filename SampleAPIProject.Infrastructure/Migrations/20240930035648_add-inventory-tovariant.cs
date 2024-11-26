using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addinventorytovariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InventoryID",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_VariantID",
                table: "Inventory",
                column: "VariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory",
                column: "VariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_VariantID",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "InventoryID",
                table: "ProductVariant");
        }
    }
}
