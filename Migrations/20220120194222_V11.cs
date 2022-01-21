using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage");

            migrationBuilder.AlterColumn<int>(
                name: "ProdavnicaID",
                table: "Storage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage",
                column: "ProdavnicaID",
                principalTable: "Prodavnica",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage");

            migrationBuilder.AlterColumn<int>(
                name: "ProdavnicaID",
                table: "Storage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Prodavnica_ProdavnicaID",
                table: "Storage",
                column: "ProdavnicaID",
                principalTable: "Prodavnica",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
