using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Grad",
                table: "Grad",
                newName: "Naziv");

            migrationBuilder.RenameColumn(
                name: "Drzava",
                table: "Drzava",
                newName: "Naziv");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Grad",
                newName: "Grad");

            migrationBuilder.RenameColumn(
                name: "Naziv",
                table: "Drzava",
                newName: "Drzava");
        }
    }
}
