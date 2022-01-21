using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageID",
                table: "Storage",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Barcode",
                table: "Predmet",
                newName: "BarCode");

            migrationBuilder.RenameColumn(
                name: "PredmetID",
                table: "Predmet",
                newName: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Storage",
                newName: "StorageID");

            migrationBuilder.RenameColumn(
                name: "BarCode",
                table: "Predmet",
                newName: "Barcode");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Predmet",
                newName: "PredmetID");
        }
    }
}
