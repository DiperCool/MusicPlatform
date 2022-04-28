using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    public partial class likes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ListenerId",
                table: "Songs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SongId = table.Column<int>(type: "integer", nullable: false),
                    ListenerId = table.Column<int>(type: "integer", nullable: false),
                    LikedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Likes_Accounts_ListenerId",
                        column: x => x.ListenerId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Likes_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ListenerId",
                table: "Songs",
                column: "ListenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_ListenerId",
                table: "Likes",
                column: "ListenerId");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_SongId",
                table: "Likes",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Accounts_ListenerId",
                table: "Songs",
                column: "ListenerId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Accounts_ListenerId",
                table: "Songs");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Songs_ListenerId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "ListenerId",
                table: "Songs");
        }
    }
}
