using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOSCBot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserFallbacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFallbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramUserExternalId = table.Column<long>(type: "bigint", nullable: false),
                    Odds = table.Column<double>(type: "double precision", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFallbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFallbackSentences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserFallbackId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFallbackSentences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFallbackSentences_UserFallbacks_UserFallbackId",
                        column: x => x.UserFallbackId,
                        principalTable: "UserFallbacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFallbacks_TelegramUserExternalId",
                table: "UserFallbacks",
                column: "TelegramUserExternalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFallbackSentences_UserFallbackId",
                table: "UserFallbackSentences",
                column: "UserFallbackId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFallbackSentences");

            migrationBuilder.DropTable(
                name: "UserFallbacks");
        }
    }
}
