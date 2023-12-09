using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateDossierStates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Published",
                table: "Dossiers");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Dossiers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "Dossiers");

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Dossiers",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
