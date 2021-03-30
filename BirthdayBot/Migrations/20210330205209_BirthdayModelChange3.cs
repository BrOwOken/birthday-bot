using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayBot.Migrations
{
    public partial class BirthdayModelChange3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsYear",
                table: "Birthdays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsYear",
                table: "Birthdays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
