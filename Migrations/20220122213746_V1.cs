using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drzava",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Drzava = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drzava", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Grad",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grad", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Predmet",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BarCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cena = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predmet", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Prodavnica",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DrzavaID = table.Column<int>(type: "int", nullable: true),
                    GradID = table.Column<int>(type: "int", nullable: true),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodavnica", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Prodavnica_Drzava_DrzavaID",
                        column: x => x.DrzavaID,
                        principalTable: "Drzava",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prodavnica_Grad_GradID",
                        column: x => x.GradID,
                        principalTable: "Grad",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdavnicaID = table.Column<int>(type: "int", nullable: false),
                    PredmetID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Storage_Predmet_PredmetID",
                        column: x => x.PredmetID,
                        principalTable: "Predmet",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Storage_Prodavnica_ProdavnicaID",
                        column: x => x.ProdavnicaID,
                        principalTable: "Prodavnica",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prodavnica_DrzavaID",
                table: "Prodavnica",
                column: "DrzavaID");

            migrationBuilder.CreateIndex(
                name: "IX_Prodavnica_GradID",
                table: "Prodavnica",
                column: "GradID");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_PredmetID",
                table: "Storage",
                column: "PredmetID");

            migrationBuilder.CreateIndex(
                name: "IX_Storage_ProdavnicaID",
                table: "Storage",
                column: "ProdavnicaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "Predmet");

            migrationBuilder.DropTable(
                name: "Prodavnica");

            migrationBuilder.DropTable(
                name: "Drzava");

            migrationBuilder.DropTable(
                name: "Grad");
        }
    }
}
