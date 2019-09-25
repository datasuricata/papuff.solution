using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace papuff.datainfra.ORM.Migrations
{
    public partial class _001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Nick = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    SiteUri = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true),
                    Tell = table.Column<string>(nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    OpeningDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    Aproved = table.Column<bool>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "General",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Stage = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_General", x => x.Id);
                    table.ForeignKey(
                        name: "FK_General_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Siege",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Visibility = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageUri = table.Column<string>(nullable: true),
                    Range = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    OperationIn = table.Column<int>(nullable: false),
                    OperationTime = table.Column<int>(nullable: false),
                    Ads = table.Column<int>(nullable: false),
                    Available = table.Column<DateTime>(nullable: false),
                    Start = table.Column<DateTime>(nullable: true),
                    Ended = table.Column<DateTime>(nullable: true),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Siege", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Siege_User_OwnerId",
                        column: x => x.OwnerId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wallet",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wallet_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Building = table.Column<int>(nullable: false),
                    Number = table.Column<int>(nullable: false),
                    Complement = table.Column<int>(nullable: false),
                    AddressLine = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    StateProvince = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    CompanyId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalSchema: "Core",
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Address_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "Core",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                    Type = table.Column<int>(nullable: false),
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
                name: "IX_Address_CompanyId",
                schema: "Core",
                table: "Address",
                column: "CompanyId",
                unique: true,
                filter: "[CompanyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserId",
                schema: "Core",
                table: "Address",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_UserId",
                schema: "Core",
                table: "Company",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_UserId",
                schema: "Core",
                table: "Document",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_General_UserId",
                schema: "Core",
                table: "General",
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

            migrationBuilder.CreateIndex(
                name: "IX_Siege_OwnerId",
                schema: "Core",
                table: "Siege",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallet_UserId",
                schema: "Core",
                table: "Wallet",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "General",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Receipt",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Siege",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Company",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Wallet",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Core");
        }
    }
}
