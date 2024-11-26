using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class collectionentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Collection",
                table: "ProductEntities");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CollectionID",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CollectionEntities",
                columns: table => new
                {
                    CollectionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionEntities", x => x.CollectionID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_CollectionID",
                table: "ProductEntities",
                column: "CollectionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities",
                column: "CollectionID",
                principalTable: "CollectionEntities",
                principalColumn: "CollectionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_CollectionEntities_CollectionID",
                table: "ProductEntities");

            migrationBuilder.DropTable(
                name: "CollectionEntities");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntities_CollectionID",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "CollectionID",
                table: "ProductEntities");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Collection",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
