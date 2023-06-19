using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class entityfieldfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PostId",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_PostId",
                table: "Files",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Posts_PostId",
                table: "Files",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Posts_PostId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_PostId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Files");
        }
    }
}
