using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Storage",
                newName: "StorageID");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Predmet",
                newName: "PredmetID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StorageID",
                table: "Storage",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PredmetID",
                table: "Predmet",
                newName: "ID");
        }
    }
}
