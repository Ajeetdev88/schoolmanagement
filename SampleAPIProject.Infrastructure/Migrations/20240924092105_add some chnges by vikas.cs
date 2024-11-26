using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addsomechngesbyvikas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_Categories_CategoryID",
                table: "ProductEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_ProductEntities_ProductID",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagsId",
                table: "ProductTags");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "TagsId",
                table: "ProductTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductTags",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CollectionID",
                table: "ProductEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "ProductEntities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_Categories_CategoryID",
                table: "ProductEntities",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities",
                column: "CollectionID",
                principalTable: "CollectionEntities",
                principalColumn: "CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_ProductEntities_ProductID",
                table: "ProductTags",
                column: "ProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagsId",
                table: "ProductTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "TagID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_Categories_CategoryID",
                table: "ProductEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_ProductEntities_ProductID",
                table: "ProductTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Tags_TagsId",
                table: "ProductTags");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TagsId",
                table: "ProductTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductTags",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CollectionID",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryID",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_Categories_CategoryID",
                table: "ProductEntities",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities",
                column: "CollectionID",
                principalTable: "CollectionEntities",
                principalColumn: "CollectionID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_ProductEntities_ProductID",
                table: "ProductTags",
                column: "ProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Tags_TagsId",
                table: "ProductTags",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "TagID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
