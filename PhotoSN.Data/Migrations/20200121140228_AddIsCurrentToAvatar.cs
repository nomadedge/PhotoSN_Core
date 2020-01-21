using Microsoft.EntityFrameworkCore.Migrations;

namespace PhotoSN.Data.Migrations
{
    public partial class AddIsCurrentToAvatar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrent",
                table: "Avatars",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrent",
                table: "Avatars");
        }
    }
}
