using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdatePTOHistoryField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "TypeCD",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "after",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "total",
                table: "UserPTOHistory");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "UserPTOHistory",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "before",
                table: "UserPTOHistory",
                newName: "Before");

            migrationBuilder.AlterColumn<decimal>(
                name: "Before",
                table: "UserPTOHistory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Count",
                table: "UserPTOHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Current",
                table: "UserPTOHistory",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsVerified",
                table: "UserPTOHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTime",
                table: "UserPTOHistory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory",
                column: "UserPTOId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "Current",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "IsVerified",
                table: "UserPTOHistory");

            migrationBuilder.DropColumn(
                name: "UpdatedTime",
                table: "UserPTOHistory");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "UserPTOHistory",
                newName: "comment");

            migrationBuilder.RenameColumn(
                name: "Before",
                table: "UserPTOHistory",
                newName: "before");

            migrationBuilder.AlterColumn<int>(
                name: "before",
                table: "UserPTOHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "TypeCD",
                table: "UserPTOHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "after",
                table: "UserPTOHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "total",
                table: "UserPTOHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserPTOHistory_UserPTOId",
                table: "UserPTOHistory",
                column: "UserPTOId",
                unique: true);
        }
    }
}
