using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_API.Migrations
{
    public partial class tabelNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kommentar_Bruger_BrugerID",
                table: "Kommentar");

            migrationBuilder.DropForeignKey(
                name: "FK_Kommentar_Tråde_TrådID",
                table: "Kommentar");

            migrationBuilder.DropForeignKey(
                name: "FK_Tråde_Bruger_BrugerID",
                table: "Tråde");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kommentar",
                table: "Kommentar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bruger",
                table: "Bruger");

            migrationBuilder.RenameTable(
                name: "Kommentar",
                newName: "Kommentarer");

            migrationBuilder.RenameTable(
                name: "Bruger",
                newName: "Brugerer");

            migrationBuilder.RenameIndex(
                name: "IX_Kommentar_TrådID",
                table: "Kommentarer",
                newName: "IX_Kommentarer_TrådID");

            migrationBuilder.RenameIndex(
                name: "IX_Kommentar_BrugerID",
                table: "Kommentarer",
                newName: "IX_Kommentarer_BrugerID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dato",
                table: "Tråde",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TrådID",
                table: "Kommentarer",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kommentarer",
                table: "Kommentarer",
                column: "KommentarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Brugerer",
                table: "Brugerer",
                column: "BrugerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kommentarer_Brugerer_BrugerID",
                table: "Kommentarer",
                column: "BrugerID",
                principalTable: "Brugerer",
                principalColumn: "BrugerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kommentarer_Tråde_TrådID",
                table: "Kommentarer",
                column: "TrådID",
                principalTable: "Tråde",
                principalColumn: "TrådID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tråde_Brugerer_BrugerID",
                table: "Tråde",
                column: "BrugerID",
                principalTable: "Brugerer",
                principalColumn: "BrugerID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kommentarer_Brugerer_BrugerID",
                table: "Kommentarer");

            migrationBuilder.DropForeignKey(
                name: "FK_Kommentarer_Tråde_TrådID",
                table: "Kommentarer");

            migrationBuilder.DropForeignKey(
                name: "FK_Tråde_Brugerer_BrugerID",
                table: "Tråde");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kommentarer",
                table: "Kommentarer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Brugerer",
                table: "Brugerer");

            migrationBuilder.RenameTable(
                name: "Kommentarer",
                newName: "Kommentar");

            migrationBuilder.RenameTable(
                name: "Brugerer",
                newName: "Bruger");

            migrationBuilder.RenameIndex(
                name: "IX_Kommentarer_TrådID",
                table: "Kommentar",
                newName: "IX_Kommentar_TrådID");

            migrationBuilder.RenameIndex(
                name: "IX_Kommentarer_BrugerID",
                table: "Kommentar",
                newName: "IX_Kommentar_BrugerID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Dato",
                table: "Tråde",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<long>(
                name: "TrådID",
                table: "Kommentar",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kommentar",
                table: "Kommentar",
                column: "KommentarID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bruger",
                table: "Bruger",
                column: "BrugerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Kommentar_Bruger_BrugerID",
                table: "Kommentar",
                column: "BrugerID",
                principalTable: "Bruger",
                principalColumn: "BrugerID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kommentar_Tråde_TrådID",
                table: "Kommentar",
                column: "TrådID",
                principalTable: "Tråde",
                principalColumn: "TrådID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tråde_Bruger_BrugerID",
                table: "Tråde",
                column: "BrugerID",
                principalTable: "Bruger",
                principalColumn: "BrugerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
