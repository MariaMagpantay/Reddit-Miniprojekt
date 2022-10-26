using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_API.Migrations
{
    public partial class Nogetsejtombruger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bruger",
                columns: table => new
                {
                    BrugerID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrugerNavn = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bruger", x => x.BrugerID);
                });

            migrationBuilder.CreateTable(
                name: "Tråde",
                columns: table => new
                {
                    TrådID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BrugerID = table.Column<long>(type: "INTEGER", nullable: false),
                    Overskrift = table.Column<string>(type: "TEXT", nullable: false),
                    Stemmer = table.Column<long>(type: "INTEGER", nullable: false),
                    Dato = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Indhold = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tråde", x => x.TrådID);
                    table.ForeignKey(
                        name: "FK_Tråde_Bruger_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Bruger",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kommentar",
                columns: table => new
                {
                    KommentarID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TrådID = table.Column<long>(type: "INTEGER", nullable: false),
                    Tekst = table.Column<string>(type: "TEXT", nullable: false),
                    BrugerID = table.Column<long>(type: "INTEGER", nullable: false),
                    Stemmer = table.Column<long>(type: "INTEGER", nullable: false),
                    Dato = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kommentar", x => x.KommentarID);
                    table.ForeignKey(
                        name: "FK_Kommentar_Bruger_BrugerID",
                        column: x => x.BrugerID,
                        principalTable: "Bruger",
                        principalColumn: "BrugerID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kommentar_Tråde_TrådID",
                        column: x => x.TrådID,
                        principalTable: "Tråde",
                        principalColumn: "TrådID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kommentar_BrugerID",
                table: "Kommentar",
                column: "BrugerID");

            migrationBuilder.CreateIndex(
                name: "IX_Kommentar_TrådID",
                table: "Kommentar",
                column: "TrådID");

            migrationBuilder.CreateIndex(
                name: "IX_Tråde_BrugerID",
                table: "Tråde",
                column: "BrugerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kommentar");

            migrationBuilder.DropTable(
                name: "Tråde");

            migrationBuilder.DropTable(
                name: "Bruger");
        }
    }
}
