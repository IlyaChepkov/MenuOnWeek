using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuOnWeek.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFileMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Recipes",
                newName: "FileId");

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_FileId",
                table: "Recipes",
                column: "FileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_Files_FileId",
                table: "Recipes",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_Files_FileId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_FileId",
                table: "Recipes");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Recipes",
                newName: "Image");
        }
    }
}
