using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateCourseGroupings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseGroupingId",
                table: "Courses",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseGroupings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommonIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RequiredCredits = table.Column<string>(type: "text", nullable: false),
                    IsTopLevel = table.Column<bool>(type: "boolean", nullable: false),
                    School = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    State = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: true),
                    Published = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupings", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "CourseGroupingReferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildGroupCommonIdentifier = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseGroupingId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupingReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseGroupingReferences_CourseGroupings_CourseGroupingId",
                        column: x => x.CourseGroupingId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CourseGroupingReferences_CourseGroupings_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseIdentifiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConcordiaCourseId = table.Column<int>(type: "integer", nullable: false),
                    CourseGroupingId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseIdentifiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseIdentifiers_CourseGroupings_CourseGroupingId",
                        column: x => x.CourseGroupingId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseGroupingId",
                table: "Courses",
                column: "CourseGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingCourseGrouping_SubGroupingsId",
                table: "CourseGroupingCourseGrouping",
                column: "SubGroupingsId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingReferences_CourseGroupingId",
                table: "CourseGroupingReferences",
                column: "CourseGroupingId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingReferences_ParentGroupId",
                table: "CourseGroupingReferences",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseIdentifiers_CourseGroupingId",
                table: "CourseIdentifiers",
                column: "CourseGroupingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroupings_CourseGroupingId",
                table: "Courses",
                column: "CourseGroupingId",
                principalTable: "CourseGroupings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroupings_CourseGroupingId",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseGroupingCourseGrouping");

            migrationBuilder.DropTable(
                name: "CourseGroupingReferences");

            migrationBuilder.DropTable(
                name: "CourseIdentifiers");

            migrationBuilder.DropTable(
                name: "CourseGroupings");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseGroupingId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseGroupingId",
                table: "Courses");
        }
    }
}
