using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElevenNote.Data.Migrations
{
    public partial class CategoryToNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Categories_CategoryId",
                table: "Notes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Categories_CategoryId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Notes");
        }
    }
}
