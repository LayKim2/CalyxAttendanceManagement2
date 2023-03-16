using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class SetForiegnKeySetUserPTO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserPTO_UserId",
                table: "UserPTO",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPTO_Users_UserId",
                table: "UserPTO",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPTO_Users_UserId",
                table: "UserPTO");

            migrationBuilder.DropIndex(
                name: "IX_UserPTO_UserId",
                table: "UserPTO");
        }
    }
}
