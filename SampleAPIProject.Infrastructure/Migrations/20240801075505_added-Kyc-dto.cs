using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedKycdto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalDate",
                table: "KYCEnttities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "KYCEnttities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "KYCEnttities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "KYCEnttities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "KYCEnttities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmitionDate",
                table: "KYCEnttities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalDate",
                table: "KYCEnttities");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "KYCEnttities");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "KYCEnttities");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "KYCEnttities");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "KYCEnttities");

            migrationBuilder.DropColumn(
                name: "SubmitionDate",
                table: "KYCEnttities");
        }
    }
}
