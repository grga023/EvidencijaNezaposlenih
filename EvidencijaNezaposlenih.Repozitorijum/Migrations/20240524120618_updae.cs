using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidencijaNezaposlenih.Repozitorijum.Migrations
{
    /// <inheritdoc />
    public partial class updae : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pozicija",
                table: "RadniOdnosi",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Struka",
                table: "RadniOdnosi",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Zanimanje",
                table: "Nezaposleni",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pozicija",
                table: "RadniOdnosi");

            migrationBuilder.DropColumn(
                name: "Struka",
                table: "RadniOdnosi");

            migrationBuilder.DropColumn(
                name: "Zanimanje",
                table: "Nezaposleni");
        }
    }
}
