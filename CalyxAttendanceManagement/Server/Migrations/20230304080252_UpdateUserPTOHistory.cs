using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdateUserPTOHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Before",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "CountType",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "UserPTOHistory");

            migrationBuilder.RenameColumn(
                name: "Current",
                table: "UserPTOHistory",
                newName: "NeedPTO");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "UserPTOHistory",
                newName: "CurrentPTO");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifiedDate",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "VerifiedDate",
                table: "UserPTOHistory");

            migrationBuilder.RenameColumn(
                name: "NeedPTO",
                table: "UserPTOHistory",
                newName: "Current");

            migrationBuilder.RenameColumn(
                name: "CurrentPTO",
                table: "UserPTOHistory",
                newName: "Count");

            migrationBuilder.AddColumn<decimal>(
                name: "Before",
                table: "UserPTOHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "CountType",
                table: "UserPTOHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
