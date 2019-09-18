using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace papuff.datainfra.Migrations
{
    public partial class _000003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Account",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Agency",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "DateDue",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Document",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Card = table.Column<string>(nullable: true),
                    Expiration = table.Column<DateTime>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    DateDue = table.Column<int>(nullable: false),
                    Document = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    WalletId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Core",
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Agency = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    DateDue = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    WalletId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Wallet_WalletId",
                        column: x => x.WalletId,
                        principalSchema: "Core",
                        principalTable: "Wallet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                schema: "Core",
                table: "Wallet",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_WalletId",
                schema: "Core",
                table: "Payment",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_WalletId",
                schema: "Core",
                table: "Receipt",
                column: "WalletId",
                unique: true,
                filter: "[WalletId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "Core");

            migrationBuilder.DropIndex(
                name: "IX_Wallet_UserId",
                schema: "Core",
                table: "Wallet");

            migrationBuilder.AddColumn<string>(
                name: "Account",
                schema: "Core",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Agency",
                schema: "Core",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DateDue",
                schema: "Core",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Document",
                schema: "Core",
                table: "Wallet",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Core",
                table: "Wallet",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                schema: "Core",
                table: "Wallet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                schema: "Core",
                table: "Wallet",
                column: "UserId");
        }
    }
}
