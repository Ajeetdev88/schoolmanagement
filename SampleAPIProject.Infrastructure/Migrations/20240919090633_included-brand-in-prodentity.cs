using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class includedbrandinprodentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandNameBrandID",
                table: "ProductEntities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_BrandNameBrandID",
                table: "ProductEntities",
                column: "BrandNameBrandID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_Brands_BrandNameBrandID",
                table: "ProductEntities",
                column: "BrandNameBrandID",
                principalTable: "Brands",
                principalColumn: "BrandID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_Brands_BrandNameBrandID",
                table: "ProductEntities");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntities_BrandNameBrandID",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "BrandNameBrandID",
                table: "ProductEntities");
        }
    }
}
