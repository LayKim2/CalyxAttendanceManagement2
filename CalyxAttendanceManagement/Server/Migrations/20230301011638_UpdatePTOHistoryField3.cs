using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdatePTOHistoryField3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserPTOHistory",
                newName: "PTOType");

            migrationBuilder.AddColumn<string>(
                name: "CountType",
                table: "UserPTOHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountType",
                table: "UserPTOHistory");

            migrationBuilder.RenameColumn(
                name: "PTOType",
                table: "UserPTOHistory",
                newName: "Type");
        }
    }
}
