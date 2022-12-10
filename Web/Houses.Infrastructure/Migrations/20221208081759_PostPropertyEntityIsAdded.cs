using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class PostPropertyEntityIsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserProperties_Posts_PostId",
                table: "ApplicationUserProperties");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Posts_PostId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Properties_PropertyId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PropertyId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUserProperties_PostId",
                table: "ApplicationUserProperties");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "ApplicationUserProperties");

            migrationBuilder.CreateTable(
                name: "p",
                columns: table => new
                {
                    PropertyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_p", x => new { x.PropertyId, x.PostId });
                    table.ForeignKey(
                        name: "FK_p_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_p_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_p_Properties_PostId",
                        column: x => x.PostId,
                        principalTable: "Properties",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_p_ApplicationUserId",
                table: "p",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_p_PostId",
                table: "p",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "p");

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostId",
                table: "ApplicationUserProperties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostId",
                table: "Posts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PropertyId",
                table: "Posts",
                column: "PropertyId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Posts_PostId",
                table: "Posts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Properties_PropertyId",
                table: "Posts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
