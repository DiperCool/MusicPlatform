using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    public partial class subscriptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Subscribers",
                table: "Accounts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ListenerId = table.Column<int>(type: "integer", nullable: true),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    SubscribedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscribers_Accounts_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscribers_Accounts_ListenerId",
                        column: x => x.ListenerId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_ArtistId",
                table: "Subscribers",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribers_ListenerId",
                table: "Subscribers",
                column: "ListenerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropColumn(
                name: "Subscribers",
                table: "Accounts");
        }
    }
}
