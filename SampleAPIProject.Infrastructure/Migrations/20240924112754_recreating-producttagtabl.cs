using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class recreatingproducttagtabl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
     
    
            migrationBuilder.CreateTable(
                name: "ProductTags",
                columns: table => new
                {
                    ProductTagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: true),
                    TagID = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Createdby = table.Column<int>(type: "int", nullable: true),
                    Modifiedby = table.Column<int>(type: "int", nullable: true),
                    TagsTagID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTags", x => x.ProductTagID);
                    table.ForeignKey(
                        name: "FK_ProductTags_ProductEntities_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductEntities",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductTags_Tags_TagsTagID",
                        column: x => x.TagsTagID,
                        principalTable: "Tags",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_TagsTagID",
                table: "ProductTags",
                column: "TagsTagID");

         
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "ProductTags");

            migrationBuilder.DropTable(
            name: "Tags");
       
        }
    }
}
