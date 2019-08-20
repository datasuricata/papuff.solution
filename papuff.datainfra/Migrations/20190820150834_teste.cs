using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace papuff.datainfra.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                schema: "Core",
                table: "General",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "BirthDate",
                schema: "Core",
                table: "General",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
