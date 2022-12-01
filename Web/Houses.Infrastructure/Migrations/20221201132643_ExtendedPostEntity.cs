using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class ExtendedPostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Posts",
                newName: "CreatedOn");

            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PropertyId",
                table: "Posts",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Properties_PropertyId",
                table: "Posts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Properties_PropertyId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PropertyId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Posts",
                newName: "Date");
        }
    }
}
