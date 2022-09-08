using Microsoft.EntityFrameworkCore.Migrations;

namespace loginwithauthentication.Migrations
{
    public partial class UserNameChangedMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Userame",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Userame",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
