using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayBot.Migrations
{
    public partial class BirthdayModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Birthdays");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Birthdays",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsYear",
                table: "Birthdays",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Birthdays");

            migrationBuilder.DropColumn(
                name: "IsYear",
                table: "Birthdays");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Birthdays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "Day",
                table: "Birthdays",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "Month",
                table: "Birthdays",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Birthdays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Birthdays_Users_UserId",
                table: "Birthdays",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
