using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class attrvaluedefinition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductEntities_ProductID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductAttributes",
                newName: "VariantID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_ProductID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_VariantID");

            migrationBuilder.AddColumn<int>(
                name: "AttributeDefinitionID",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttributeDefinitionID1",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttributeValueID",
                table: "ProductAttributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AttributeValueID1",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductEntitiesProductID",
                table: "ProductAttributes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AttributeDefinitions",
                columns: table => new
                {
                    AttributeDefinitionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeDefinitions", x => x.AttributeDefinitionID);
                });

            migrationBuilder.CreateTable(
                name: "AttributeValues",
                columns: table => new
                {
                    AttributeValueID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttributeDefinitionID = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeValues", x => x.AttributeValueID);
                    table.ForeignKey(
                        name: "FK_AttributeValues_AttributeDefinitions_AttributeDefinitionID",
                        column: x => x.AttributeDefinitionID,
                        principalTable: "AttributeDefinitions",
                        principalColumn: "AttributeDefinitionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeDefinitionID",
                table: "ProductAttributes",
                column: "AttributeDefinitionID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeDefinitionID1",
                table: "ProductAttributes",
                column: "AttributeDefinitionID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeValueID",
                table: "ProductAttributes",
                column: "AttributeValueID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_AttributeValueID1",
                table: "ProductAttributes",
                column: "AttributeValueID1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAttributes_ProductEntitiesProductID",
                table: "ProductAttributes",
                column: "ProductEntitiesProductID");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeValues_AttributeDefinitionID",
                table: "AttributeValues",
                column: "AttributeDefinitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeDefinitions_AttributeDefinitionID",
                table: "ProductAttributes",
                column: "AttributeDefinitionID",
                principalTable: "AttributeDefinitions",
                principalColumn: "AttributeDefinitionID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeDefinitions_AttributeDefinitionID1",
                table: "ProductAttributes",
                column: "AttributeDefinitionID1",
                principalTable: "AttributeDefinitions",
                principalColumn: "AttributeDefinitionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID",
                table: "ProductAttributes",
                column: "AttributeValueID",
                principalTable: "AttributeValues",
                principalColumn: "AttributeValueID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID1",
                table: "ProductAttributes",
                column: "AttributeValueID1",
                principalTable: "AttributeValues",
                principalColumn: "AttributeValueID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeDefinitions_AttributeDefinitionID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeDefinitions_AttributeDefinitionID1",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_AttributeValues_AttributeValueID1",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductEntities_ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductAttributes_ProductVariant_VariantID",
                table: "ProductAttributes");

            migrationBuilder.DropTable(
                name: "AttributeValues");

            migrationBuilder.DropTable(
                name: "AttributeDefinitions");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeDefinitionID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeDefinitionID1",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeValueID",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_AttributeValueID1",
                table: "ProductAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ProductAttributes_ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeDefinitionID1",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeValueID",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeValueID1",
                table: "ProductAttributes");

            migrationBuilder.DropColumn(
                name: "ProductEntitiesProductID",
                table: "ProductAttributes");

            migrationBuilder.RenameColumn(
                name: "VariantID",
                table: "ProductAttributes",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductAttributes_VariantID",
                table: "ProductAttributes",
                newName: "IX_ProductAttributes_ProductID");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "ProductVariant",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "ProductAttributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductAttributes_ProductEntities_ProductID",
                table: "ProductAttributes",
                column: "ProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
