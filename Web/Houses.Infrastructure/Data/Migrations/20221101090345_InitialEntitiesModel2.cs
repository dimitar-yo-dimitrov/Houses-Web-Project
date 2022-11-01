using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Data.Migrations
{
    public partial class InitialEntitiesModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertiesTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertiesTypes",
                table: "PropertiesTypes");

            migrationBuilder.RenameTable(
                name: "PropertiesTypes",
                newName: "PropertyTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes");

            migrationBuilder.RenameTable(
                name: "PropertyTypes",
                newName: "PropertiesTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertiesTypes",
                table: "PropertiesTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertiesTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertiesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
