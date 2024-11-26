using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addeduserIdtokycentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "KYCEnttities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "KYCEnttities");
        }
    }
}
