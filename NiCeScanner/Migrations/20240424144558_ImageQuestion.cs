using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NiCeScanner.Migrations
{
    /// <inheritdoc />
    public partial class ImageQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Questions");

            migrationBuilder.AddColumn<long>(
                name: "ImageId",
                table: "Questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Uuid",
                table: "Images",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ImageId",
                table: "Questions",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Images_ImageId",
                table: "Questions",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Images_ImageId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ImageId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Uuid",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Questions",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
