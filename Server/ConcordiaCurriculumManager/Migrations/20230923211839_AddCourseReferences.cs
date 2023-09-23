using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class AddCourseReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0e926b23-77a8-4ebe-a066-1599d91696b5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("23ffe188-2d91-4b8b-8dfd-bee6bf56c31d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("36b7f570-8674-4ed8-8809-96b2f475f193"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("4bc39865-9eac-48fc-a712-4fa9b4103897"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("4e7ccef5-7085-4fc6-89e3-f3b110ad2096"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("511ca93c-4bf9-43ad-a734-06a95b243d54"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("57217549-8112-4fda-8c27-876a2235be28"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5f9d5ac4-9744-4213-8d50-e198b291122a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("67c60f9a-596a-4195-834e-b9d8cac30f8d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("80f256b6-acbd-473a-a50d-21bb5ad69b2b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8dc1f72a-12bb-4ed5-aa9c-71212fbdcbfd"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("901ee4da-c5c3-46ed-b6ea-2fead13c2f64"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9ba472c9-4679-4c5a-8ff2-076cfea64e09"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a11646af-2c24-4c24-8f00-6db0d263c8d1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("afdc1405-6719-4c93-a1e5-6bf7b6eb824c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b7836b6c-d6eb-4ed4-915e-f5cbcefda6a1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cb796e74-9ddf-4959-b176-c709180f9966"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f9366a9e-d10f-4272-987e-cf0f76aa1f70"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("faee414a-45db-47f4-93a4-7c07077b4c4a"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("91a246e2-f75b-41f4-9678-7093535eaa08"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9fd315d3-65a4-4718-91f2-ffa33e069744"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("d580aa57-6184-4ce0-8e5d-c557a2e259a2"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("91a246e2-f75b-41f4-9678-7093535eaa08"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9fd315d3-65a4-4718-91f2-ffa33e069744"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d580aa57-6184-4ce0-8e5d-c557a2e259a2"));

            migrationBuilder.CreateTable(
                name: "CourseReferences",
                columns: table => new
                {
                    CourseID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CourseReferencingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseReferences", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_CourseReferences_Courses_CourseReferencingId",
                        column: x => x.CourseReferencingId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("04528cec-fb9d-4a41-bf7c-179578d8dba7"), 1, "Field Studies", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8849), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8850) },
                    { new Guid("0ebce308-810c-48e6-baaa-7144195a4429"), 2, "Fieldwork", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8852), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8852) },
                    { new Guid("13321ab8-9a57-4ad6-a236-7f567274991b"), 17, "Tutorial/Lab", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8900), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8901) },
                    { new Guid("42399ad1-d4e1-4dc5-b53e-a3bfd3dc21bf"), 18, "Workshop", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8903), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8903) },
                    { new Guid("440a9fd4-50ce-40fa-a130-29062059ba10"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8875), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8876) },
                    { new Guid("47b79ccc-8a33-439e-a0d9-ca4022c56b05"), 0, "Conference", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8843), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8844) },
                    { new Guid("4d092dd4-a32c-485b-8ef9-b785bd09db10"), 11, "Regular", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8885), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8886) },
                    { new Guid("503ab4ba-1ee0-46e6-940c-6a5259d7fb5a"), 14, "Studio", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8893), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8893) },
                    { new Guid("5857b83a-126d-4dbf-aa5f-8d8d117e6473"), 10, "Reading", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8880), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8881) },
                    { new Guid("6b3c8951-44cc-47b5-83fe-9db88bbbc8af"), 13, "Seminar", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8890), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8891) },
                    { new Guid("9ea71f9b-6cdb-4a1c-8731-29e9c944b314"), 7, "Online", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8873), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8874) },
                    { new Guid("b2a436eb-a49e-49ee-9161-a85170c7982c"), 4, "Laboratory", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8866), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8866) },
                    { new Guid("b817e1e2-f52e-493d-a258-fa1f0a712b3f"), 12, "Research", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8888), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8888) },
                    { new Guid("bf1214e1-9333-4d85-bd32-d48cae4e5000"), 6, "Modular", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8871), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8872) },
                    { new Guid("e033e74c-8a24-4914-9d32-82649906ebd9"), 16, "Tutorial", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8898), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8898) },
                    { new Guid("f22ef1bb-4670-44de-a547-0652f9065392"), 3, "Independent Study", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8864), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8864) },
                    { new Guid("f7246d4c-c0de-4b0b-9f98-753d8781d5ee"), 15, "Thesis Research", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8895), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8895) },
                    { new Guid("fb6ef9a9-4cca-4409-857d-44735684d3db"), 5, "Lecture", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8869), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8870) },
                    { new Guid("fbdc7787-bf80-4139-8501-6b9309059f21"), 9, "Private Studies", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8878), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8878) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("41e6ae67-7ad8-4407-a5dd-c4f5ebd9786c"), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8249), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8249), RoleEnum.FacultyMember },
                    { new Guid("bf4b66cd-0c71-4737-96bf-76ba6220df44"), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8246), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8247), RoleEnum.Admin },
                    { new Guid("c8b69129-6320-4cfe-9766-4417061fbb9a"), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8239), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8242), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8329), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8330) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8345), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8345) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("41e6ae67-7ad8-4407-a5dd-c4f5ebd9786c"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("bf4b66cd-0c71-4737-96bf-76ba6220df44"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("c8b69129-6320-4cfe-9766-4417061fbb9a"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseID",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CourseReferences_CourseReferencingId",
                table: "CourseReferences",
                column: "CourseReferencingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseReferences_CourseID",
                table: "Courses",
                column: "CourseID",
                principalTable: "CourseReferences",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseReferences_CourseID",
                table: "Courses");

            migrationBuilder.DropTable(
                name: "CourseReferences");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseID",
                table: "Courses");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("04528cec-fb9d-4a41-bf7c-179578d8dba7"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0ebce308-810c-48e6-baaa-7144195a4429"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("13321ab8-9a57-4ad6-a236-7f567274991b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("42399ad1-d4e1-4dc5-b53e-a3bfd3dc21bf"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("440a9fd4-50ce-40fa-a130-29062059ba10"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("47b79ccc-8a33-439e-a0d9-ca4022c56b05"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("4d092dd4-a32c-485b-8ef9-b785bd09db10"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("503ab4ba-1ee0-46e6-940c-6a5259d7fb5a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5857b83a-126d-4dbf-aa5f-8d8d117e6473"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6b3c8951-44cc-47b5-83fe-9db88bbbc8af"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9ea71f9b-6cdb-4a1c-8731-29e9c944b314"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b2a436eb-a49e-49ee-9161-a85170c7982c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b817e1e2-f52e-493d-a258-fa1f0a712b3f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("bf1214e1-9333-4d85-bd32-d48cae4e5000"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e033e74c-8a24-4914-9d32-82649906ebd9"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f22ef1bb-4670-44de-a547-0652f9065392"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f7246d4c-c0de-4b0b-9f98-753d8781d5ee"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fb6ef9a9-4cca-4409-857d-44735684d3db"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fbdc7787-bf80-4139-8501-6b9309059f21"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("41e6ae67-7ad8-4407-a5dd-c4f5ebd9786c"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("bf4b66cd-0c71-4737-96bf-76ba6220df44"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("c8b69129-6320-4cfe-9766-4417061fbb9a"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("41e6ae67-7ad8-4407-a5dd-c4f5ebd9786c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bf4b66cd-0c71-4737-96bf-76ba6220df44"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c8b69129-6320-4cfe-9766-4417061fbb9a"));

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("0e926b23-77a8-4ebe-a066-1599d91696b5"), 15, "Thesis Research", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5043), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5044) },
                    { new Guid("23ffe188-2d91-4b8b-8dfd-bee6bf56c31d"), 2, "Fieldwork", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4920), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4921) },
                    { new Guid("36b7f570-8674-4ed8-8809-96b2f475f193"), 17, "Tutorial/Lab", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5058), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5059) },
                    { new Guid("4bc39865-9eac-48fc-a712-4fa9b4103897"), 6, "Modular", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4978), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4979) },
                    { new Guid("4e7ccef5-7085-4fc6-89e3-f3b110ad2096"), 11, "Regular", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5019), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5020) },
                    { new Guid("511ca93c-4bf9-43ad-a734-06a95b243d54"), 18, "Workshop", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5242), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5243) },
                    { new Guid("57217549-8112-4fda-8c27-876a2235be28"), 9, "Private Studies", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4999), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5000) },
                    { new Guid("5f9d5ac4-9744-4213-8d50-e198b291122a"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4990), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4991) },
                    { new Guid("67c60f9a-596a-4195-834e-b9d8cac30f8d"), 5, "Lecture", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4971), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4972) },
                    { new Guid("80f256b6-acbd-473a-a50d-21bb5ad69b2b"), 12, "Research", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5025), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5026) },
                    { new Guid("8dc1f72a-12bb-4ed5-aa9c-71212fbdcbfd"), 4, "Laboratory", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4959), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4960) },
                    { new Guid("901ee4da-c5c3-46ed-b6ea-2fead13c2f64"), 1, "Field Studies", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4913), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4914) },
                    { new Guid("9ba472c9-4679-4c5a-8ff2-076cfea64e09"), 13, "Seminar", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5031), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5032) },
                    { new Guid("a11646af-2c24-4c24-8f00-6db0d263c8d1"), 0, "Conference", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4898), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4901) },
                    { new Guid("afdc1405-6719-4c93-a1e5-6bf7b6eb824c"), 16, "Tutorial", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5049), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5050) },
                    { new Guid("b7836b6c-d6eb-4ed4-915e-f5cbcefda6a1"), 10, "Reading", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5005), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5006) },
                    { new Guid("cb796e74-9ddf-4959-b176-c709180f9966"), 3, "Independent Study", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4953), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4954) },
                    { new Guid("f9366a9e-d10f-4272-987e-cf0f76aa1f70"), 7, "Online", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4984), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(4985) },
                    { new Guid("faee414a-45db-47f4-93a4-7c07077b4c4a"), 14, "Studio", new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5037), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(5038) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("91a246e2-f75b-41f4-9678-7093535eaa08"), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2551), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2552), RoleEnum.Admin },
                    { new Guid("9fd315d3-65a4-4718-91f2-ffa33e069744"), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2561), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2562), RoleEnum.FacultyMember },
                    { new Guid("d580aa57-6184-4ce0-8e5d-c557a2e259a2"), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2529), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2535), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2755), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2756) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2901), new DateTime(2023, 9, 19, 2, 21, 2, 668, DateTimeKind.Utc).AddTicks(2902) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("91a246e2-f75b-41f4-9678-7093535eaa08"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("9fd315d3-65a4-4718-91f2-ffa33e069744"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("d580aa57-6184-4ce0-8e5d-c557a2e259a2"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
