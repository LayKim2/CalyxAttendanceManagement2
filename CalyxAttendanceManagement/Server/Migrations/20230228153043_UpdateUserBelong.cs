﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalyxAttendanceManagement.Server.Migrations
{
    public partial class UpdateUserBelong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Belong",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Belong",
                table: "Users");
        }
    }
}
