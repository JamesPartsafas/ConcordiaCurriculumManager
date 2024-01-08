using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateManyToManyCourseIdentifiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseIdentifiers_CourseGroupings_CourseGroupingId",
                table: "CourseIdentifiers");

            migrationBuilder.DropIndex(
                name: "IX_CourseIdentifiers_CourseGroupingId",
                table: "CourseIdentifiers");

            migrationBuilder.DropColumn(
                name: "CourseGroupingId",
                table: "CourseIdentifiers");

            migrationBuilder.CreateTable(
                name: "CourseGroupingCourseIdentifier",
                columns: table => new
                {
                    CourseGroupingId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseIdentifiersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupingCourseIdentifier", x => new { x.CourseGroupingId, x.CourseIdentifiersId });
                    table.ForeignKey(
                        name: "FK_CourseGroupingCourseIdentifier_CourseGroupings_CourseGroupi~",
                        column: x => x.CourseGroupingId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGroupingCourseIdentifier_CourseIdentifiers_CourseIden~",
                        column: x => x.CourseIdentifiersId,
                        principalTable: "CourseIdentifiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingCourseIdentifier_CourseIdentifiersId",
                table: "CourseGroupingCourseIdentifier",
                column: "CourseIdentifiersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroupingCourseIdentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseGroupingId",
                table: "CourseIdentifiers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseIdentifiers_CourseGroupingId",
                table: "CourseIdentifiers",
                column: "CourseGroupingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseIdentifiers_CourseGroupings_CourseGroupingId",
                table: "CourseIdentifiers",
                column: "CourseGroupingId",
                principalTable: "CourseGroupings",
                principalColumn: "Id");
        }
    }
}
