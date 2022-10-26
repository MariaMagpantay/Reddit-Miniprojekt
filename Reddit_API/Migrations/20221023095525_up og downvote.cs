using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reddit_API.Migrations
{
    public partial class upogdownvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stemmer",
                table: "Tråde",
                newName: "UpVotes");

            migrationBuilder.RenameColumn(
                name: "Stemmer",
                table: "Kommentarer",
                newName: "UpVotes");

            migrationBuilder.AddColumn<long>(
                name: "DownVotes",
                table: "Tråde",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DownVotes",
                table: "Kommentarer",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Tråde");

            migrationBuilder.DropColumn(
                name: "DownVotes",
                table: "Kommentarer");

            migrationBuilder.RenameColumn(
                name: "UpVotes",
                table: "Tråde",
                newName: "Stemmer");

            migrationBuilder.RenameColumn(
                name: "UpVotes",
                table: "Kommentarer",
                newName: "Stemmer");
        }
    }
}
