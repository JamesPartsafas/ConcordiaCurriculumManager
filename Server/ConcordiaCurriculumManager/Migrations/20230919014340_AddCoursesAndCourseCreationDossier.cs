using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCoursesAndCourseCreationDossier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("10165e67-fe83-4d81-88d8-65f026485768"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("6631d19c-4946-455f-a407-93856911349b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e92ecb44-418a-48a4-b4df-82e1f35e0689"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("10165e67-fe83-4d81-88d8-65f026485768"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6631d19c-4946-455f-a407-93856911349b"));

            migrationBuilder.CreateTable(
                name: "CourseComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentCode = table.Column<int>(type: "integer", nullable: false),
                    ComponentName = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseID = table.Column<int>(type: "integer", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Catalog = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreditValue = table.Column<string>(type: "text", nullable: false),
                    PreReqs = table.Column<string>(type: "text", nullable: false),
                    Career = table.Column<int>(type: "integer", nullable: false),
                    EquivalentCourses = table.Column<string>(type: "text", nullable: false),
                    CourseState = table.Column<int>(type: "integer", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Published = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "CourseCreationDossiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewCourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCreationDossiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCreationDossiers_Courses_NewCourseId",
                        column: x => x.NewCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCreationDossiers_Users_InitiatorId",
                        column: x => x.InitiatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("15dccf3e-bb40-4c83-9cc9-1a44c06442b8"), 13, "Seminar", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1263), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1264) },
                    { new Guid("32834c9f-513c-472a-a58b-52973fe9ac0b"), 16, "Tutorial", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1280), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1281) },
                    { new Guid("351b6279-f0db-4ad7-8027-baffc048fbaf"), 7, "Online", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1215), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1216) },
                    { new Guid("420282c4-7604-4a62-baa7-3525b90ee356"), 12, "Research", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1257), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1258) },
                    { new Guid("59b2ac3a-b02c-47e7-9f67-087427e1a331"), 6, "Modular", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1209), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1209) },
                    { new Guid("6a6dcdb5-d85e-4073-99ba-1dbb1004594b"), 11, "Regular", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1251), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1252) },
                    { new Guid("7867c383-c2c6-462d-8331-9c69506c31d3"), 9, "Private Studies", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1230), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1231) },
                    { new Guid("8f9456d2-5b42-418d-8908-8a8bd8e99078"), 1, "Field Studies", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1147), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1148) },
                    { new Guid("976ddabe-458a-4027-aab1-4a28b9323063"), 15, "Thesis Research", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1275), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1275) },
                    { new Guid("9cead84f-9656-448e-9534-d007848ab8c8"), 0, "Conference", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1133), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1135) },
                    { new Guid("9f34a24c-586c-4102-8667-6a8efad8da4b"), 17, "Tutorial/Lab", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1288), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1289) },
                    { new Guid("afc3f669-562e-4359-8559-cc31ea6a83e5"), 3, "Independent Study", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1185), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1186) },
                    { new Guid("b6a02ff4-ab79-43a0-b8c8-de7d017d6e1b"), 2, "Fieldwork", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1179), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1179) },
                    { new Guid("ba2fe855-b30d-4df6-a6cc-c58820107d68"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1221), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1222) },
                    { new Guid("ca63bbcb-0d6f-4731-a412-a60c96176d59"), 18, "Workshop", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1301), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1302) },
                    { new Guid("cbb2b0d3-1706-479d-8d0e-8aaa728ddd2a"), 5, "Lecture", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1202), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1203) },
                    { new Guid("e672cc87-ee26-4b90-b34d-0103cdd3b739"), 10, "Reading", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1245), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1245) },
                    { new Guid("ecfb9ee8-35f9-4bed-9f96-769e0c5ab6c8"), 14, "Studio", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1269), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1270) },
                    { new Guid("f058d35d-a3e5-4cb7-89d7-857f76760bab"), 4, "Laboratory", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1191), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1192) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8617), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8626), RoleEnum.Initiator },
                    { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8639), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8640), RoleEnum.Admin },
                    { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8646), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8647), RoleEnum.FacultyMember }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8828), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8829) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8872), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8872) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCourseComponent_CoursesId",
                table: "CourseCourseComponent",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationDossiers_InitiatorId",
                table: "CourseCreationDossiers",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationDossiers_NewCourseId",
                table: "CourseCreationDossiers",
                column: "NewCourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCourseComponent");

            migrationBuilder.DropTable(
                name: "CourseCreationDossiers");

            migrationBuilder.DropTable(
                name: "CourseComponents");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("10165e67-fe83-4d81-88d8-65f026485768"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2413), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2414), RoleEnum.Admin },
                    { new Guid("6631d19c-4946-455f-a407-93856911349b"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2401), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2406), RoleEnum.Initiator },
                    { new Guid("e92ecb44-418a-48a4-b4df-82e1f35e0689"), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2418), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2419), RoleEnum.FacultyMember }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2555), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2556) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2789), new DateTime(2023, 9, 16, 4, 22, 7, 802, DateTimeKind.Utc).AddTicks(2790) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("10165e67-fe83-4d81-88d8-65f026485768"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("6631d19c-4946-455f-a407-93856911349b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
