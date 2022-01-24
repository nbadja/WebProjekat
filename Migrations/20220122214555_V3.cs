using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DrzavaID",
                table: "Grad",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grad_DrzavaID",
                table: "Grad",
                column: "DrzavaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Grad_Drzava_DrzavaID",
                table: "Grad",
                column: "DrzavaID",
                principalTable: "Drzava",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grad_Drzava_DrzavaID",
                table: "Grad");

            migrationBuilder.DropIndex(
                name: "IX_Grad_DrzavaID",
                table: "Grad");

            migrationBuilder.DropColumn(
                name: "DrzavaID",
                table: "Grad");
        }
    }
}
