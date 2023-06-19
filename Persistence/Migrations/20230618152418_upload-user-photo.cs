using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class uploaduserphoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePhotoId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Files_ProfilePhotoId",
                table: "AspNetUsers",
                column: "ProfilePhotoId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Files_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePhotoId",
                table: "AspNetUsers");
        }
    }
}
