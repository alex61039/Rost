using Microsoft.EntityFrameworkCore.Migrations;

namespace Rost.Repository.Migrations
{
    public partial class UserPhotosLocalPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "UserPhotos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "UserPhotos");
        }
    }
}
