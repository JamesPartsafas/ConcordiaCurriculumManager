using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateGroupingRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseGroupingRequest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseGroupingId = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestType = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DossierId = table.Column<Guid>(type: "uuid", nullable: false),
                    Rationale = table.Column<string>(type: "text", nullable: false),
                    ResourceImplication = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Conflict = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseGroupingRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseGroupingRequest_CourseGroupings_CourseGroupingId",
                        column: x => x.CourseGroupingId,
                        principalTable: "CourseGroupings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseGroupingRequest_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingRequest_CourseGroupingId",
                table: "CourseGroupingRequest",
                column: "CourseGroupingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroupingRequest_DossierId",
                table: "CourseGroupingRequest",
                column: "DossierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseGroupingRequest");
        }
    }
}
