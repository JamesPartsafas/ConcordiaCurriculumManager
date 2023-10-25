using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class GroupMasters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupUser1",
                columns: table => new
                {
                    GroupMastersId = table.Column<Guid>(type: "uuid", nullable: false),
                    MasteredGroupsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupUser1", x => new { x.GroupMastersId, x.MasteredGroupsId });
                    table.ForeignKey(
                        name: "FK_GroupUser1_Groups_MasteredGroupsId",
                        column: x => x.MasteredGroupsId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupUser1_Users_GroupMastersId",
                        column: x => x.GroupMastersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupUser1_MasteredGroupsId",
                table: "GroupUser1",
                column: "MasteredGroupsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupUser1");
        }
    }
}
