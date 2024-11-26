using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedTableForCartAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_ProductEntities_ProductID",
                table: "ProductVariant");

            migrationBuilder.DropIndex(
                name: "IX_Inventory_ProductVariantVariantID",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "InventoryID",
                table: "ProductVariant");

            migrationBuilder.DropColumn(
                name: "ProductVariantVariantID",
                table: "Inventory");

            migrationBuilder.AlterColumn<string>(
                name: "buttonname",
                table: "WelcomeMessages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "WelcomeMessages",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductVariant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Locality",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Landmark",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShippingAddressID = table.Column<int>(type: "int", nullable: true),
                    BillingAddressID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_BillingAddressID",
                        column: x => x.BillingAddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_ShippingAddressID",
                        column: x => x.ShippingAddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressId");
                    table.ForeignKey(
                        name: "FK_Orders_UserAuths_UserId",
                        column: x => x.UserId,
                        principalTable: "UserAuths",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppinCart",
                columns: table => new
                {
                    CartID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppinCart", x => x.CartID);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    OrderItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    VariantID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.OrderItemID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_ProductEntities_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductEntities",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_ProductVariant_VariantID",
                        column: x => x.VariantID,
                        principalTable: "ProductVariant",
                        principalColumn: "VariantID");
                });

            migrationBuilder.CreateTable(
                name: "ShoppinCartItem",
                columns: table => new
                {
                    CartItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    VariantID = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShoppingCartCartID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppinCartItem", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_ShoppinCartItem_ProductEntities_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductEntities",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppinCartItem_ProductVariant_VariantID",
                        column: x => x.VariantID,
                        principalTable: "ProductVariant",
                        principalColumn: "VariantID");
                    table.ForeignKey(
                        name: "FK_ShoppinCartItem_ShoppinCart_ShoppingCartCartID",
                        column: x => x.ShoppingCartCartID,
                        principalTable: "ShoppinCart",
                        principalColumn: "CartID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductID",
                table: "OrderItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_VariantID",
                table: "OrderItem",
                column: "VariantID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BillingAddressID",
                table: "Orders",
                column: "BillingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ShippingAddressID",
                table: "Orders",
                column: "ShippingAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinCartItem_ProductID",
                table: "ShoppinCartItem",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinCartItem_ShoppingCartCartID",
                table: "ShoppinCartItem",
                column: "ShoppingCartCartID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppinCartItem_VariantID",
                table: "ShoppinCartItem",
                column: "VariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory",
                column: "VariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_ProductEntities_ProductID",
                table: "ProductVariant",
                column: "ProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariant_ProductEntities_ProductID",
                table: "ProductVariant");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ShoppinCartItem");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ShoppinCart");

            migrationBuilder.AlterColumn<string>(
                name: "buttonname",
                table: "WelcomeMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Heading",
                table: "WelcomeMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductID",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InventoryID",
                table: "ProductVariant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ProductEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductVariantVariantID",
                table: "Inventory",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Locality",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Landmark",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductVariantVariantID",
                table: "Inventory",
                column: "ProductVariantVariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_ProductVariantVariantID",
                table: "Inventory",
                column: "ProductVariantVariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_ProductVariant_VariantID",
                table: "Inventory",
                column: "VariantID",
                principalTable: "ProductVariant",
                principalColumn: "VariantID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariant_ProductEntities_ProductID",
                table: "ProductVariant",
                column: "ProductID",
                principalTable: "ProductEntities",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
