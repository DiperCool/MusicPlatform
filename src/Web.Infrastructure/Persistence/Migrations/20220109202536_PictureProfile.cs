using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Infrastructure.Migrations
{
    public partial class PictureProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Profiles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_PictureId",
                table: "Profiles",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_PathesToFiles_PictureId",
                table: "Profiles",
                column: "PictureId",
                principalTable: "PathesToFiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_PathesToFiles_PictureId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_PictureId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Profiles");
        }
    }
}
