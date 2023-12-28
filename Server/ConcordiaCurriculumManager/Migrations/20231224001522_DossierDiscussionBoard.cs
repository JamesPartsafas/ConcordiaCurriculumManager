using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class DossierDiscussionBoard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DossierDiscussion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DossierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DossierDiscussion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DossierDiscussion_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionMessage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DossierDiscussionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentDiscussionMessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscussionMessage_DiscussionMessage_ParentDiscussionMessage~",
                        column: x => x.ParentDiscussionMessageId,
                        principalTable: "DiscussionMessage",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiscussionMessage_DossierDiscussion_DossierDiscussionId",
                        column: x => x.DossierDiscussionId,
                        principalTable: "DossierDiscussion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionMessage_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionMessage_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionMessage_AuthorId",
                table: "DiscussionMessage",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionMessage_DossierDiscussionId",
                table: "DiscussionMessage",
                column: "DossierDiscussionId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionMessage_GroupId",
                table: "DiscussionMessage",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionMessage_ParentDiscussionMessageId",
                table: "DiscussionMessage",
                column: "ParentDiscussionMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_DossierDiscussion_DossierId",
                table: "DossierDiscussion",
                column: "DossierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscussionMessage");

            migrationBuilder.DropTable(
                name: "DossierDiscussion");
        }
    }
}
