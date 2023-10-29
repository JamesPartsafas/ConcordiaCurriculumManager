using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateSupportingFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseComponent");

            migrationBuilder.AddColumn<string>(
                name: "CourseNotes",
                table: "Courses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CourseModificationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rationale",
                table: "CourseModificationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceImplication",
                table: "CourseModificationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "CourseCreationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rationale",
                table: "CourseCreationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ResourceImplication",
                table: "CourseCreationRequests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CourseComponents_ComponentCode",
                table: "CourseComponents",
                column: "ComponentCode");

            migrationBuilder.CreateTable(
                name: "CourseCourseComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentCode = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    HoursPerWeek = table.Column<int>(type: "integer", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCourseComponents_CourseComponents_ComponentCode",
                        column: x => x.ComponentCode,
                        principalTable: "CourseComponents",
                        principalColumn: "ComponentCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseComponents_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SupportingFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentBase64 = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupportingFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupportingFiles_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseComponents_ComponentCode",
                table: "CourseCourseComponents",
                column: "ComponentCode");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseComponents_CourseId",
                table: "CourseCourseComponents",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SupportingFiles_CourseId",
                table: "SupportingFiles",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseComponents");

            migrationBuilder.DropTable(
                name: "SupportingFiles");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CourseComponents_ComponentCode",
                table: "CourseComponents");

            migrationBuilder.DropColumn(
                name: "CourseNotes",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CourseModificationRequests");

            migrationBuilder.DropColumn(
                name: "Rationale",
                table: "CourseModificationRequests");

            migrationBuilder.DropColumn(
                name: "ResourceImplication",
                table: "CourseModificationRequests");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "CourseCreationRequests");

            migrationBuilder.DropColumn(
                name: "Rationale",
                table: "CourseCreationRequests");

            migrationBuilder.DropColumn(
                name: "ResourceImplication",
                table: "CourseCreationRequests");

            migrationBuilder.CreateTable(
                name: "CourseCourseComponent",
                columns: table => new
                {
                    CourseComponentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CoursesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCourseComponent", x => new { x.CourseComponentsId, x.CoursesId });
                    table.ForeignKey(
                        name: "FK_CourseCourseComponent_CourseComponents_CourseComponentsId",
                        column: x => x.CourseComponentsId,
                        principalTable: "CourseComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCourseComponent_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseComponent_CoursesId",
                table: "CourseCourseComponent",
                column: "CoursesId");
        }
    }
}
