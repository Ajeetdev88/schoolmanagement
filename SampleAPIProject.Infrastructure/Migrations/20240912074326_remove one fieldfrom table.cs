using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MLMProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeonefieldfromtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageurl",
                table: "WelcomeMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageurl",
                table: "WelcomeMessages",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
