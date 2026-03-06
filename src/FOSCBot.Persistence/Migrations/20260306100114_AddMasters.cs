using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOSCBot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramUserExternalId = table.Column<long>(type: "bigint", nullable: false),
                    AuthenticatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Masters_TelegramUserExternalId",
                table: "Masters",
                column: "TelegramUserExternalId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Masters");
        }
    }
}
