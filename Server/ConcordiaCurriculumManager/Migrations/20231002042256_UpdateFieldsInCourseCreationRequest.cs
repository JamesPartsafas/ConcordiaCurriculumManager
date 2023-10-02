using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsInCourseCreationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("01bfe37c-fb2c-4956-b5aa-e0fb05023905"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("053cab82-5c6f-461e-8e21-8e587bf6d396"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0764a497-e8dd-4dad-86f9-55c81793f012"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("182b57e4-9085-4e12-b73a-1792aa8da77a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("2731560c-8272-43f8-96d4-1720e9b3fffa"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("342b75c6-dad1-40b2-87cb-61e4fceceb6d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("38fd5a41-e9e4-4952-8162-f77cd4e3efa7"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("47370be6-9915-4eff-8f41-6871a9dea0d6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("557813ed-b41f-44e2-8fc8-29fad17371bb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7168d061-6395-4472-a04b-0a8d3ec9fc8e"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7ac45e1a-cfac-4eab-9280-5bb074df3dd0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8ebd77f7-bced-4e44-8c5f-e3c12c2dee2c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("af62346d-8c64-416c-84c1-97b25d2040ea"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b5b8de1c-e54f-4ec8-a69d-12f0f48f4c39"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b7a84f21-250d-4662-9b50-37f507141b22"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("bf4f40f0-0de1-42a2-88d6-f68a17c867a6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cfe7bacc-5bf9-45cf-a65c-4ec2a2a29d98"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fa455046-9c1b-4ca8-9957-a4452e127dfb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fc70ad1e-ea15-4da0-b0e3-5ef274694489"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3fc0f322-a086-4ebd-b077-518d88d5781c"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("c6d98eac-4184-4e5a-8adc-7feda86e0e3b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("cf2a8f76-c52d-440e-9314-c00447fd733d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3fc0f322-a086-4ebd-b077-518d88d5781c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c6d98eac-4184-4e5a-8adc-7feda86e0e3b"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cf2a8f76-c52d-440e-9314-c00447fd733d"));

            migrationBuilder.DropColumn(
                name: "InitiatorId",
                table: "CourseCreationRequests");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "InitiatorId",
                table: "CourseCreationRequests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("01bfe37c-fb2c-4956-b5aa-e0fb05023905"), 12, "Research", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1313), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1314) },
                    { new Guid("053cab82-5c6f-461e-8e21-8e587bf6d396"), 7, "Online", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1300), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1301) },
                    { new Guid("0764a497-e8dd-4dad-86f9-55c81793f012"), 13, "Seminar", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1335), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1335) },
                    { new Guid("182b57e4-9085-4e12-b73a-1792aa8da77a"), 15, "Thesis Research", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1339), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1339) },
                    { new Guid("2731560c-8272-43f8-96d4-1720e9b3fffa"), 10, "Reading", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1309), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1310) },
                    { new Guid("342b75c6-dad1-40b2-87cb-61e4fceceb6d"), 16, "Tutorial", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1341), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1341) },
                    { new Guid("38fd5a41-e9e4-4952-8162-f77cd4e3efa7"), 3, "Independent Study", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1291), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1291) },
                    { new Guid("47370be6-9915-4eff-8f41-6871a9dea0d6"), 0, "Conference", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1273), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1274) },
                    { new Guid("557813ed-b41f-44e2-8fc8-29fad17371bb"), 11, "Regular", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1311), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1312) },
                    { new Guid("7168d061-6395-4472-a04b-0a8d3ec9fc8e"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1302), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1303) },
                    { new Guid("7ac45e1a-cfac-4eab-9280-5bb074df3dd0"), 1, "Field Studies", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1285), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1285) },
                    { new Guid("8ebd77f7-bced-4e44-8c5f-e3c12c2dee2c"), 18, "Workshop", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1347), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1347) },
                    { new Guid("af62346d-8c64-416c-84c1-97b25d2040ea"), 4, "Laboratory", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1293), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1293) },
                    { new Guid("b5b8de1c-e54f-4ec8-a69d-12f0f48f4c39"), 9, "Private Studies", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1307), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1308) },
                    { new Guid("b7a84f21-250d-4662-9b50-37f507141b22"), 17, "Tutorial/Lab", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1345), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1345) },
                    { new Guid("bf4f40f0-0de1-42a2-88d6-f68a17c867a6"), 2, "Fieldwork", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1288), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1288) },
                    { new Guid("cfe7bacc-5bf9-45cf-a65c-4ec2a2a29d98"), 6, "Modular", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1298), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1299) },
                    { new Guid("fa455046-9c1b-4ca8-9957-a4452e127dfb"), 14, "Studio", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1337), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1337) },
                    { new Guid("fc70ad1e-ea15-4da0-b0e3-5ef274694489"), 5, "Lecture", new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1296), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(1297) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3fc0f322-a086-4ebd-b077-518d88d5781c"), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(687), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(688), RoleEnum.Admin },
                    { new Guid("c6d98eac-4184-4e5a-8adc-7feda86e0e3b"), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(679), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(681), RoleEnum.Initiator },
                    { new Guid("cf2a8f76-c52d-440e-9314-c00447fd733d"), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(690), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(690), RoleEnum.FacultyMember }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(797), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(797) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(813), new DateTime(2023, 10, 2, 2, 37, 27, 990, DateTimeKind.Utc).AddTicks(813) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3fc0f322-a086-4ebd-b077-518d88d5781c"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("c6d98eac-4184-4e5a-8adc-7feda86e0e3b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("cf2a8f76-c52d-440e-9314-c00447fd733d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
