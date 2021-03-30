using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayBot.Migrations
{
    public partial class BirthdayModelChange4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsYear",
                table: "Birthdays",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsYear",
                table: "Birthdays");
        }
    }
}
