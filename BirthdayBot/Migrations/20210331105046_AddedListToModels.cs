using Microsoft.EntityFrameworkCore.Migrations;

namespace BirthdayBot.Migrations
{
    public partial class AddedListToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NamedayUser",
                columns: table => new
                {
                    WatchedNamedaysId = table.Column<int>(type: "int", nullable: false),
                    WatchersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NamedayUser", x => new { x.WatchedNamedaysId, x.WatchersId });
                    table.ForeignKey(
                        name: "FK_NamedayUser_Namedays_WatchedNamedaysId",
                        column: x => x.WatchedNamedaysId,
                        principalTable: "Namedays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NamedayUser_Users_WatchersId",
                        column: x => x.WatchersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Birthdays_UserId",
                table: "Birthdays",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NamedayUser_WatchersId",
                table: "NamedayUser",
                column: "WatchersId");

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

            migrationBuilder.DropTable(
                name: "NamedayUser");

            migrationBuilder.DropIndex(
                name: "IX_Birthdays_UserId",
                table: "Birthdays");
        }
    }
}
