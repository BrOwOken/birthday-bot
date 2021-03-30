using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayBot.Migrations
{
    public partial class BirthdayModelChange2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays");

            migrationBuilder.DropForeignKey(
                name: "FK_Namedays_Users_UserId",
                table: "Namedays");

            migrationBuilder.DropIndex(
                name: "IX_Namedays_UserId",
                table: "Namedays");

            migrationBuilder.DropIndex(
                name: "IX_Birthdays_UserId",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Namedays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Namedays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Namedays_UserId",
                table: "Namedays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Birthdays_UserId",
                table: "Birthdays",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Namedays_Users_UserId",
                table: "Namedays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
