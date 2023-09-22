using System;
using ConcordiaCurriculumManager.Models.Users;
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
                .Annotation("Npgsql:Enum:role_enum", "Initiator,Admin,FacultyMember");

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
                    { new Guid("10165e67-fe83-4d81-88d8-65f026485768"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2413), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2414), RoleEnum.Admin },
                    { new Guid("6631d19c-4946-455f-a407-93856911349b"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2401), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2406), RoleEnum.Initiator },
                    { new Guid("e92ecb44-418a-48a4-b4df-82e1f35e0689"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2418), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2419), RoleEnum.FacultyMember }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "LastName", "ModifiedDate", "Password" },
                values: new object[,]
                {
                    { new Guid("37581d9d-713f-475c-9668-23971b0e64d0"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2555), "admin@ccm.ca", "Super", "User", new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2556), "9767718E8A58C097D48ED8986E632368F71F71740C6DCE113AE75ED90176DA49:FE06FEFB87C75014327930CFB3373565" },
                    { new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2789), "joe.user@ccm.ca", "Joe", "User", new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2790), "DAFBF72A150765D4DDDB5089E2D8516F5C68A00DD77930F2F4C013CB89DB8E77:B497E6DD99B7DD2ED2632F5A136A8788" }
                });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("10165e67-fe83-4d81-88d8-65f026485768"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("6631d19c-4946-455f-a407-93856911349b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
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
