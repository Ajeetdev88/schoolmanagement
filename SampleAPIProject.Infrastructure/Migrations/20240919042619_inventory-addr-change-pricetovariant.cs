using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inventoryaddrchangepricetovariant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductEntities_ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductVariant_VariantID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ProductVariant",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductVariant",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsBillingAddress",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsShippingAddress",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductVariant_VariantID",
                table: "ProductAttributes",
                column: "VariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductVariant_VariantID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "IsBillingAddress",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "IsShippingAddress",
                table: "Addresses");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ProductEntities",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductEntities",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductEntitiesProductID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductEntitiesProductID",
                table: "ProductAttributes",
                column: "ProductEntitiesProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductEntities_ProductEntitiesProductID",
                table: "ProductAttributes",
                column: "ProductEntitiesProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductVariant_VariantID",
                table: "ProductAttributes",
                column: "VariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
