﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidencijaNezaposlenih.Repozitorijum.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nezaposleni",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BrojTelefona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nezaposleni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Poslodavci",
                columns: table => new
                {
                    PIB = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Naziv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poslodavci", x => x.PIB);
                });

            migrationBuilder.CreateTable(
                name: "RadniOdnosi",
                columns: table => new
                {
                    PIB = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NezaposleniID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DatumPocetka = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZavrsetka = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RadniOdnosi", x => new { x.PIB, x.NezaposleniID });
                    table.ForeignKey(
                        name: "FK_RadniOdnosi_Nezaposleni_NezaposleniID",
                        column: x => x.NezaposleniID,
                        principalTable: "Nezaposleni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RadniOdnosi_Poslodavci_PIB",
                        column: x => x.PIB,
                        principalTable: "Poslodavci",
                        principalColumn: "PIB",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RadniOdnosi_NezaposleniID",
                table: "RadniOdnosi",
                column: "NezaposleniID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RadniOdnosi");

            migrationBuilder.DropTable(
                name: "Nezaposleni");

            migrationBuilder.DropTable(
                name: "Poslodavci");
        }
    }
}
