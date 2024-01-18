using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class LabelGroupingReferenceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroupingReferences_CourseGroupings_CourseGroupingId",
                table: "CourseGroupingReferences");

            migrationBuilder.DropTable(
                name: "CourseGroupingCourseGrouping");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroupingReferences_CourseGroupingId",
                table: "CourseGroupingReferences");

            migrationBuilder.DropColumn(
                name: "CourseGroupingId",
                table: "CourseGroupingReferences");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseGroupingId",
                table: "CourseGroupings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GroupingType",
                table: "CourseGroupingReferences",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupings_CourseGroupingId",
                table: "CourseGroupings",
                column: "CourseGroupingId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroupings_CourseGroupings_CourseGroupingId",
                table: "CourseGroupings",
                column: "CourseGroupingId",
                principalTable: "CourseGroupings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroupings_CourseGroupings_CourseGroupingId",
                table: "CourseGroupings");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroupings_CourseGroupingId",
                table: "CourseGroupings");

            migrationBuilder.DropColumn(
                name: "CourseGroupingId",
                table: "CourseGroupings");

            migrationBuilder.DropColumn(
                name: "GroupingType",
                table: "CourseGroupingReferences");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseGroupingId",
                table: "CourseGroupingReferences",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseGroupingCourseGrouping",
                columns: table => new
                {
                    OptionalGroupingsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubGroupingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupingCourseGrouping", x => new { x.OptionalGroupingsId, x.SubGroupingsId });
                    table.ForeignKey(
                        name: "FK_CourseGroupingCourseGrouping_CourseGroupings_OptionalGroupi~",
                        column: x => x.OptionalGroupingsId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGroupingCourseGrouping_CourseGroupings_SubGroupingsId",
                        column: x => x.SubGroupingsId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingReferences_CourseGroupingId",
                table: "CourseGroupingReferences",
                column: "CourseGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingCourseGrouping_SubGroupingsId",
                table: "CourseGroupingCourseGrouping",
                column: "SubGroupingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroupingReferences_CourseGroupings_CourseGroupingId",
                table: "CourseGroupingReferences",
                column: "CourseGroupingId",
                principalTable: "CourseGroupings",
                principalColumn: "Id");
        }
    }
}
