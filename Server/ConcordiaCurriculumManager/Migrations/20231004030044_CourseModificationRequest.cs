using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CourseModificationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("3b4fdb95-8257-42b4-8f72-a86dd597f418"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("480897a2-423c-4e42-8a19-e557de1d5d73"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("619f79c0-3920-4802-af91-f479fa058756"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("61eb6aca-74ff-4c8f-8afe-2b940d8537f3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("63b42a50-52f1-42dd-80c6-184b262cf894"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("663f174f-48cf-4cf5-bf07-697f6b632209"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6f3faecb-3253-4d9e-9629-b2491182a6da"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8844bbbf-a244-4add-96d4-332c6a5abfc8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9afd068c-dcf0-4137-861d-2f4e2be40937"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9f2e530b-04e8-4fef-a2af-5838c5dbf70e"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a08443e9-0a71-44b3-bd70-e1e52a573a94"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a342bd02-c9ec-43de-accf-fa36ad9762a9"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("bc906976-5817-485e-9319-ff6b7f6cb07a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c610d3e4-402b-4e29-a25f-06dcaf468e01"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c989fbf3-39f2-4db9-8478-91f899a2f380"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d09c48d2-00f1-41aa-9236-17cb12ce3a11"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f281afe7-bfc2-4642-b9d7-65b5f83e6b75"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fb82c910-3047-4c71-9f89-5847816914d9"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fe407502-68bc-4923-8e8a-9661edd95a98"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("2ee354fb-fa04-4e3b-9f69-1e505b8c2c6b"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("48887908-740b-4694-ad8f-ce42f7543665"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("c5249001-ed33-44be-9492-844d0361dc08"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2ee354fb-fa04-4e3b-9f69-1e505b8c2c6b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("48887908-740b-4694-ad8f-ce42f7543665"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c5249001-ed33-44be-9492-844d0361dc08"));

            migrationBuilder.CreateTable(
                name: "CourseModificationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DossierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseModificationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseModificationRequests_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseModificationRequests_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("004878a5-af45-4d22-85d6-bfa33f2ced21"), 13, "Seminar", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9950), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9950) },
                    { new Guid("026cb6f1-519b-4c79-8bbd-e5d65c689138"), 4, "Laboratory", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9850), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9850) },
                    { new Guid("0c27f379-e9a5-4def-8760-801903df90e8"), 9, "Private Studies", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9910), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9910) },
                    { new Guid("1e3e6d8b-a5ec-4c8c-bc68-ba22de17a264"), 3, "Independent Study", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9830), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9830) },
                    { new Guid("31bd8982-23a6-4bd2-b01c-3c7afcb2246a"), 12, "Research", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9940), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9940) },
                    { new Guid("41a56649-2a8c-41f4-b514-612a673aaf36"), 0, "Conference", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9790), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9790) },
                    { new Guid("46ce75e7-28fd-4ee8-a29e-99c36853a42f"), 5, "Lecture", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9860), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9860) },
                    { new Guid("5e046a4b-1946-4ad9-9531-89780a1ae0ea"), 10, "Reading", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9920), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9920) },
                    { new Guid("5e0c5821-9adc-4b0d-b017-abfe99bd05bf"), 11, "Regular", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9930), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9930) },
                    { new Guid("5ed5ab3a-4ab7-4ee9-bad0-85fa6f810e68"), 6, "Modular", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9870), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9870) },
                    { new Guid("72603e20-0cc8-423b-a734-d89b506eb800"), 1, "Field Studies", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9810), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9810) },
                    { new Guid("7e3786d7-6a87-4a1b-b758-a14691c6f1b5"), 17, "Tutorial/Lab", new DateTime(2023, 10, 4, 3, 0, 44, 345, DateTimeKind.Utc).AddTicks(10), new DateTime(2023, 10, 4, 3, 0, 44, 345, DateTimeKind.Utc).AddTicks(10) },
                    { new Guid("833447bd-a76a-44fa-a7fc-e1be469aedc6"), 2, "Fieldwork", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9820), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9820) },
                    { new Guid("84b8726f-1117-41b7-b706-d962eb952382"), 7, "Online", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9880), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9880) },
                    { new Guid("92724877-5614-41a1-a614-de24d621b040"), 18, "Workshop", new DateTime(2023, 10, 4, 3, 0, 44, 345, DateTimeKind.Utc).AddTicks(20), new DateTime(2023, 10, 4, 3, 0, 44, 345, DateTimeKind.Utc).AddTicks(20) },
                    { new Guid("a9d28400-ae2e-4e08-8d80-9bb5e7b0cde6"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9890), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9890) },
                    { new Guid("c15ee139-e696-4933-9c7f-ace8b85da525"), 15, "Thesis Research", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9980), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9980) },
                    { new Guid("c87de41e-82f5-4f16-8741-9ba4d8540df1"), 16, "Tutorial", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9990), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9990) },
                    { new Guid("d113746c-808c-4814-ae0f-1dc59f55c87a"), 14, "Studio", new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9970), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9970) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("513b0d86-86b4-4808-99f0-3f97a77b65f3"), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9250), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9250), RoleEnum.Admin },
                    { new Guid("9db8ca41-2846-4d72-bde4-96d9302cfb4f"), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9260), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9260), RoleEnum.FacultyMember },
                    { new Guid("d4b87914-32a6-478a-b1b3-61f78e6a6707"), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9230), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9230), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9400), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9400) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9420), new DateTime(2023, 10, 4, 3, 0, 44, 344, DateTimeKind.Utc).AddTicks(9420) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("513b0d86-86b4-4808-99f0-3f97a77b65f3"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("9db8ca41-2846-4d72-bde4-96d9302cfb4f"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("d4b87914-32a6-478a-b1b3-61f78e6a6707"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseModificationRequests_CourseId",
                table: "CourseModificationRequests",
                column: "CourseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseModificationRequests_DossierId",
                table: "CourseModificationRequests",
                column: "DossierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseModificationRequests");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("004878a5-af45-4d22-85d6-bfa33f2ced21"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("026cb6f1-519b-4c79-8bbd-e5d65c689138"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0c27f379-e9a5-4def-8760-801903df90e8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1e3e6d8b-a5ec-4c8c-bc68-ba22de17a264"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("31bd8982-23a6-4bd2-b01c-3c7afcb2246a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("41a56649-2a8c-41f4-b514-612a673aaf36"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("46ce75e7-28fd-4ee8-a29e-99c36853a42f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5e046a4b-1946-4ad9-9531-89780a1ae0ea"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5e0c5821-9adc-4b0d-b017-abfe99bd05bf"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5ed5ab3a-4ab7-4ee9-bad0-85fa6f810e68"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("72603e20-0cc8-423b-a734-d89b506eb800"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7e3786d7-6a87-4a1b-b758-a14691c6f1b5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("833447bd-a76a-44fa-a7fc-e1be469aedc6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("84b8726f-1117-41b7-b706-d962eb952382"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("92724877-5614-41a1-a614-de24d621b040"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a9d28400-ae2e-4e08-8d80-9bb5e7b0cde6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c15ee139-e696-4933-9c7f-ace8b85da525"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c87de41e-82f5-4f16-8741-9ba4d8540df1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d113746c-808c-4814-ae0f-1dc59f55c87a"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("513b0d86-86b4-4808-99f0-3f97a77b65f3"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9db8ca41-2846-4d72-bde4-96d9302cfb4f"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("d4b87914-32a6-478a-b1b3-61f78e6a6707"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("513b0d86-86b4-4808-99f0-3f97a77b65f3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9db8ca41-2846-4d72-bde4-96d9302cfb4f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d4b87914-32a6-478a-b1b3-61f78e6a6707"));

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("3b4fdb95-8257-42b4-8f72-a86dd597f418"), 5, "Lecture", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4650), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4650) },
                    { new Guid("480897a2-423c-4e42-8a19-e557de1d5d73"), 10, "Reading", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4665), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4665) },
                    { new Guid("619f79c0-3920-4802-af91-f479fa058756"), 11, "Regular", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4667), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4667) },
                    { new Guid("61eb6aca-74ff-4c8f-8afe-2b940d8537f3"), 15, "Thesis Research", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4677), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4677) },
                    { new Guid("63b42a50-52f1-42dd-80c6-184b262cf894"), 3, "Independent Study", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4644), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4645) },
                    { new Guid("663f174f-48cf-4cf5-bf07-697f6b632209"), 9, "Private Studies", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4663), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4663) },
                    { new Guid("6f3faecb-3253-4d9e-9629-b2491182a6da"), 4, "Laboratory", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4646), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4647) },
                    { new Guid("8844bbbf-a244-4add-96d4-332c6a5abfc8"), 0, "Conference", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4632), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4633) },
                    { new Guid("9afd068c-dcf0-4137-861d-2f4e2be40937"), 7, "Online", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4658), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4658) },
                    { new Guid("9f2e530b-04e8-4fef-a2af-5838c5dbf70e"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4660), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4660) },
                    { new Guid("a08443e9-0a71-44b3-bd70-e1e52a573a94"), 14, "Studio", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4675), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4675) },
                    { new Guid("a342bd02-c9ec-43de-accf-fa36ad9762a9"), 16, "Tutorial", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4679), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4679) },
                    { new Guid("bc906976-5817-485e-9319-ff6b7f6cb07a"), 17, "Tutorial/Lab", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4681), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4682) },
                    { new Guid("c610d3e4-402b-4e29-a25f-06dcaf468e01"), 13, "Seminar", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4671), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4672) },
                    { new Guid("c989fbf3-39f2-4db9-8478-91f899a2f380"), 2, "Fieldwork", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4642), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4642) },
                    { new Guid("d09c48d2-00f1-41aa-9236-17cb12ce3a11"), 18, "Workshop", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4683), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4684) },
                    { new Guid("f281afe7-bfc2-4642-b9d7-65b5f83e6b75"), 12, "Research", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4669), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4670) },
                    { new Guid("fb82c910-3047-4c71-9f89-5847816914d9"), 6, "Modular", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4656), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4656) },
                    { new Guid("fe407502-68bc-4923-8e8a-9661edd95a98"), 1, "Field Studies", new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4639), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4639) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("2ee354fb-fa04-4e3b-9f69-1e505b8c2c6b"), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4020), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4020), RoleEnum.Admin },
                    { new Guid("48887908-740b-4694-ad8f-ce42f7543665"), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4023), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4023), RoleEnum.FacultyMember },
                    { new Guid("c5249001-ed33-44be-9492-844d0361dc08"), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4002), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4005), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4104), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4105) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4150), new DateTime(2023, 10, 2, 4, 22, 55, 973, DateTimeKind.Utc).AddTicks(4150) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("2ee354fb-fa04-4e3b-9f69-1e505b8c2c6b"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("48887908-740b-4694-ad8f-ce42f7543665"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("c5249001-ed33-44be-9492-844d0361dc08"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
