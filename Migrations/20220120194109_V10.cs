using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prodavnica_Storage_StorageID",
                table: "Prodavnica");

            migrationBuilder.DropIndex(
                name: "IX_Prodavnica_StorageID",
                table: "Prodavnica");

            migrationBuilder.DropColumn(
                name: "StorageID",
                table: "Prodavnica");

            migrationBuilder.AddColumn<int>(
                name: "ProdavnicaID",
                table: "Storage",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Storage_ProdavnicaID",
                table: "Storage",
                column: "ProdavnicaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage",
                column: "ProdavnicaID",
                principalTable: "Prodavnica",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Storage_ProdavnicaID",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "ProdavnicaID",
                table: "Storage");

            migrationBuilder.AddColumn<int>(
                name: "StorageID",
                table: "Prodavnica",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prodavnica_StorageID",
                table: "Prodavnica",
                column: "StorageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prodavnica_Storage_StorageID",
                table: "Prodavnica",
                column: "StorageID",
                principalTable: "Storage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
