using System;
using ConcordiaCurriculumManager.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:role_enum", "Initiator,Admin,FacultyMemeber");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserRole = table.Column<RoleEnum>(type: "role_enum", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("5f7794f2-b3ee-4b7b-a3c7-dc6aa518de8d"), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6151), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6155), RoleEnum.Initiator },
                    { new Guid("a8904e1e-1fa8-4c02-a8c1-4e1643efd608"), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6170), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6170), RoleEnum.Admin },
                    { new Guid("e7530308-e5e9-49f0-b8ea-b4aa716f8a33"), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6173), new DateTime(2023, 9, 15, 18, 13, 9, 413, DateTimeKind.Utc).AddTicks(6173), RoleEnum.FacultyMemeber }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
