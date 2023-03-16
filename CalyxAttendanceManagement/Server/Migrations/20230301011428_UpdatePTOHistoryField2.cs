using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdatePTOHistoryField2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "UserPTOHistory");

            migrationBuilder.AddColumn<string>(
                name: "VerifiedType",
                table: "UserPTOHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifiedType",
                table: "UserPTOHistory");

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "UserPTOHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
