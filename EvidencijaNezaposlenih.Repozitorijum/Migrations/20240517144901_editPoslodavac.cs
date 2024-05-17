using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvidencijaNezaposlenih.Repozitorijum.Migrations
{
    /// <inheritdoc />
    public partial class editPoslodavac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grad",
                table: "Poslodavci",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grad",
                table: "Poslodavci");
        }
    }
}
