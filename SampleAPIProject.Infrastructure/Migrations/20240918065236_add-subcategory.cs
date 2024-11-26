using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsubcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "ProductEntities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "ProductEntities");
        }
    }
}
