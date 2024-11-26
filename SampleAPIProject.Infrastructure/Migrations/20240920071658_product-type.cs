using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class producttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ProductEntities");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeTypeID",
                table: "ProductEntities",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "ProductEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    TypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.TypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntities_ProductTypeTypeID",
                table: "ProductEntities",
                column: "ProductTypeTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_TypeId",
                table: "Categories",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ProductType_TypeId",
                table: "Categories",
                column: "TypeId",
                principalTable: "ProductType",
                principalColumn: "TypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntities_ProductType_ProductTypeTypeID",
                table: "ProductEntities",
                column: "ProductTypeTypeID",
                principalTable: "ProductType",
                principalColumn: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ProductType_TypeId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntities_ProductType_ProductTypeTypeID",
                table: "ProductEntities");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntities_ProductTypeTypeID",
                table: "ProductEntities");

            migrationBuilder.DropIndex(
                name: "IX_Categories_TypeId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductTypeTypeID",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "ProductEntities");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
