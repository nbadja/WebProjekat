﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace Cenovnik.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Predmet_PredmetID",
                table: "Storage");

            migrationBuilder.AlterColumn<int>(
                name: "PredmetID",
                table: "Storage",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Predmet_PredmetID",
                table: "Storage",
                column: "PredmetID",
                principalTable: "Predmet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Predmet_PredmetID",
                table: "Storage");

            migrationBuilder.AlterColumn<int>(
                name: "PredmetID",
                table: "Storage",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Predmet_PredmetID",
                table: "Storage",
                column: "PredmetID",
                principalTable: "Predmet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}