using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExternalIdsAndImportProgress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalId",
                table: "Apartments",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImportProgresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    LastProcessedExternalId = table.Column<string>(type: "text", nullable: false),
                    ProcessedCount = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportProgresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ExternalId",
                table: "AspNetUsers",
                column: "ExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ExternalId",
                table: "Apartments",
                column: "ExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportProgresses_FileName",
                table: "ImportProgresses",
                column: "FileName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImportProgresses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ExternalId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_ExternalId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Apartments");
        }
    }
}
