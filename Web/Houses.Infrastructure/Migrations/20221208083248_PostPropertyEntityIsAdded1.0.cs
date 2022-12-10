using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class PostPropertyEntityIsAdded10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_p_AspNetUsers_ApplicationUserId",
                table: "p");

            migrationBuilder.DropForeignKey(
                name: "FK_p_Posts_PostId",
                table: "p");

            migrationBuilder.DropForeignKey(
                name: "FK_p_Properties_PostId",
                table: "p");

            migrationBuilder.DropPrimaryKey(
                name: "PK_p",
                table: "p");

            migrationBuilder.RenameTable(
                name: "p",
                newName: "PropertyPosts");

            migrationBuilder.RenameIndex(
                name: "IX_p_PostId",
                table: "PropertyPosts",
                newName: "IX_PropertyPosts_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_p_ApplicationUserId",
                table: "PropertyPosts",
                newName: "IX_PropertyPosts_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyPosts",
                table: "PropertyPosts",
                columns: new[] { "PropertyId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPosts_AspNetUsers_ApplicationUserId",
                table: "PropertyPosts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPosts_Posts_PostId",
                table: "PropertyPosts",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyPosts_Properties_PropertyId",
                table: "PropertyPosts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPosts_AspNetUsers_ApplicationUserId",
                table: "PropertyPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPosts_Posts_PostId",
                table: "PropertyPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyPosts_Properties_PropertyId",
                table: "PropertyPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyPosts",
                table: "PropertyPosts");

            migrationBuilder.RenameTable(
                name: "PropertyPosts",
                newName: "p");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyPosts_PostId",
                table: "p",
                newName: "IX_p_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyPosts_ApplicationUserId",
                table: "p",
                newName: "IX_p_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_p",
                table: "p",
                columns: new[] { "PropertyId", "PostId" });

            migrationBuilder.AddForeignKey(
                name: "FK_p_AspNetUsers_ApplicationUserId",
                table: "p",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_p_Posts_PostId",
                table: "p",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_p_Properties_PostId",
                table: "p",
                column: "PostId",
                principalTable: "Properties",
                principalColumn: "Id");
        }
    }
}
