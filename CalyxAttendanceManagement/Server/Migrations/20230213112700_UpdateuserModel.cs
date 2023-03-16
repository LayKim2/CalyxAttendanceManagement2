using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdateuserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAuthenticated",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAuthenticated",
                table: "Users");
        }
    }
}
