using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Houses.Infrastructure.Migrations
{
    public partial class PostPropertyEntityIsAdded11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Properties",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertyId",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "DeletedOn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "Posts");
        }
    }
}
