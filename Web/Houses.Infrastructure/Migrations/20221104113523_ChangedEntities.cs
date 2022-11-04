using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class ChangedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Images_ImageId",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "ImageId",
                table: "Properties",
                newName: "ImageUrlId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ImageId",
                table: "Properties",
                newName: "IX_Properties_ImageUrlId");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Images",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_PropertyId",
                table: "Images",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Images_ImageUrlId",
                table: "Properties",
                column: "ImageUrlId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Properties_PropertyId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Images_ImageUrlId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Images_PropertyId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "ImageUrlId",
                table: "Properties",
                newName: "ImageId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_ImageUrlId",
                table: "Properties",
                newName: "IX_Properties_ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Images_ImageId",
                table: "Properties",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
