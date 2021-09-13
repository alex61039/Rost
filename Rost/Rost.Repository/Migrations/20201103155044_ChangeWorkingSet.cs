using Microsoft.EntityFrameworkCore.Migrations;

namespace Rost.Repository.Migrations
{
    public partial class ChangeWorkingSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Children",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Children",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Children");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Children");
        }
    }
}
