using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdatePTOHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserPTOId",
                table: "UserPTOHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory",
                column: "UserPTOId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPTOHistory_UserPTO_UserPTOId",
                table: "UserPTOHistory",
                column: "UserPTOId",
                principalTable: "UserPTO",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPTOHistory_UserPTO_UserPTOId",
                table: "UserPTOHistory");

            migrationBuilder.DropIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "UserPTOId",
                table: "UserPTOHistory");
        }
    }
}
