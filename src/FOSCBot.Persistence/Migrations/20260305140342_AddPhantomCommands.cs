using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FOSCBot.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPhantomCommands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PhantomCommands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Personality = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhantomCommandChats",
                columns: table => new
                {
                    PhantomCommandId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatExternalId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhantomCommandChats", x => new { x.PhantomCommandId, x.ChatExternalId });
                    table.ForeignKey(
                        name: "FK_PhantomCommandChats_PhantomCommands_PhantomCommandId",
                        column: x => x.PhantomCommandId,
                        principalTable: "PhantomCommands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhantomCommands_Name",
                table: "PhantomCommands",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhantomCommandChats");

            migrationBuilder.DropTable(
                name: "PhantomCommands");
        }
    }
}
