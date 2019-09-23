using Microsoft.EntityFrameworkCore.Migrations;

namespace papuff.datainfra.Migrations
{
    public partial class migration_00004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperationIn",
                schema: "Core",
                table: "Siege",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OperationTime",
                schema: "Core",
                table: "Siege",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperationIn",
                schema: "Core",
                table: "Siege");

            migrationBuilder.DropColumn(
                name: "OperationTime",
                schema: "Core",
                table: "Siege");
        }
    }
}