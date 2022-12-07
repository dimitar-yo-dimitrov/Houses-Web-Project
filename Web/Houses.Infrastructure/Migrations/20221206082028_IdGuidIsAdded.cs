using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class IdGuidIsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "ApplicationUserProperties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserProperties_PostId",
                table: "ApplicationUserProperties",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserProperties_Posts_PostId",
                table: "ApplicationUserProperties",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProperties_Posts_PostId",
                table: "ApplicationUserProperties");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserProperties_PostId",
                table: "ApplicationUserProperties");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "ApplicationUserProperties");
        }
    }
}
