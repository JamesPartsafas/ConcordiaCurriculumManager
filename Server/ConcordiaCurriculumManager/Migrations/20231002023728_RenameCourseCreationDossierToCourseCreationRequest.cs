using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class RenameCourseCreationDossierToCourseCreationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCreationRequests_Users_InitiatorId",
                table: "CourseCreationRequests");

            migrationBuilder.DropIndex(
                name: "IX_CourseCreationRequests_InitiatorId",
                table: "CourseCreationRequests");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("38bb2be8-5d09-41d5-81e0-3fcf2419bc15"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("3d5a2ab1-e541-46fe-9d1b-d8691f684265"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("4210de11-0c8e-4b04-8f60-d7b7a14d1e2a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("606b0473-9d62-472f-bbb8-f2c13c607c89"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("774c6826-b52c-4a68-9f8c-81ae2b71194a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8df10afe-ef11-4dd7-be29-2f64b2856ed6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("93ca69bc-1b31-4ca6-a5ef-39011cbf195f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9531f2d6-aef8-4682-a3e9-2d4323b8f1a6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9b595081-bca9-459d-8355-6cb39b94063b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("adbb796c-5c1e-444b-9db7-3cd7a0563d53"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cb733f26-84f1-42f2-9bd7-54a95750a303"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e4581af3-8a01-40b4-97bd-dadebc3a264f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e6439894-e66d-4876-a4ed-c8c353c76cb8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ea792821-1686-4af3-b9f4-105ab6906efa"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ee423e14-bf86-46d0-b900-72d73f49b8d0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ef4307f5-a5d9-479b-b61f-81404609d29a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ef699266-cf27-4543-95dc-017182f3fef5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f22ba818-c939-494a-8b73-fc21215d2246"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fffabc21-6838-4629-8eed-c6964f441fc3"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("1b0eac8c-58cd-4c92-8b6c-b09bca84ff23"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("2372760c-31cf-4f34-92d5-d52d721795fe"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9525360f-e8fa-43ad-9576-71f248f10a89"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1b0eac8c-58cd-4c92-8b6c-b09bca84ff23"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("2372760c-31cf-4f34-92d5-d52d721795fe"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9525360f-e8fa-43ad-9576-71f248f10a89"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("38bb2be8-5d09-41d5-81e0-3fcf2419bc15"), 5, "Lecture", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3683), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3684) },
                    { new Guid("3d5a2ab1-e541-46fe-9d1b-d8691f684265"), 1, "Field Studies", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3630), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3631) },
                    { new Guid("4210de11-0c8e-4b04-8f60-d7b7a14d1e2a"), 18, "Workshop", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3780), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3780) },
                    { new Guid("606b0473-9d62-472f-bbb8-f2c13c607c89"), 12, "Research", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3736), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3736) },
                    { new Guid("774c6826-b52c-4a68-9f8c-81ae2b71194a"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3700), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3701) },
                    { new Guid("8df10afe-ef11-4dd7-be29-2f64b2856ed6"), 10, "Reading", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3723), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3724) },
                    { new Guid("93ca69bc-1b31-4ca6-a5ef-39011cbf195f"), 4, "Laboratory", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3672), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3673) },
                    { new Guid("9531f2d6-aef8-4682-a3e9-2d4323b8f1a6"), 15, "Thesis Research", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3754), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3754) },
                    { new Guid("9b595081-bca9-459d-8355-6cb39b94063b"), 6, "Modular", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3689), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3689) },
                    { new Guid("adbb796c-5c1e-444b-9db7-3cd7a0563d53"), 16, "Tutorial", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3760), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3760) },
                    { new Guid("cb733f26-84f1-42f2-9bd7-54a95750a303"), 17, "Tutorial/Lab", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3767), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3768) },
                    { new Guid("e4581af3-8a01-40b4-97bd-dadebc3a264f"), 3, "Independent Study", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3666), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3667) },
                    { new Guid("e6439894-e66d-4876-a4ed-c8c353c76cb8"), 9, "Private Studies", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3708), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3709) },
                    { new Guid("ea792821-1686-4af3-b9f4-105ab6906efa"), 2, "Fieldwork", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3659), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3660) },
                    { new Guid("ee423e14-bf86-46d0-b900-72d73f49b8d0"), 0, "Conference", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3616), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3619) },
                    { new Guid("ef4307f5-a5d9-479b-b61f-81404609d29a"), 14, "Studio", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3748), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3749) },
                    { new Guid("ef699266-cf27-4543-95dc-017182f3fef5"), 11, "Regular", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3729), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3730) },
                    { new Guid("f22ba818-c939-494a-8b73-fc21215d2246"), 13, "Seminar", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3742), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3743) },
                    { new Guid("fffabc21-6838-4629-8eed-c6964f441fc3"), 7, "Online", new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3694), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(3695) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("1b0eac8c-58cd-4c92-8b6c-b09bca84ff23"), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1437), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1438), RoleEnum.Admin },
                    { new Guid("2372760c-31cf-4f34-92d5-d52d721795fe"), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1419), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1424), RoleEnum.Initiator },
                    { new Guid("9525360f-e8fa-43ad-9576-71f248f10a89"), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1446), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1447), RoleEnum.FacultyMember }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1619), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1620) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1661), new DateTime(2023, 10, 2, 1, 24, 31, 8, DateTimeKind.Utc).AddTicks(1662) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("1b0eac8c-58cd-4c92-8b6c-b09bca84ff23"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("2372760c-31cf-4f34-92d5-d52d721795fe"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("9525360f-e8fa-43ad-9576-71f248f10a89"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationRequests_InitiatorId",
                table: "CourseCreationRequests",
                column: "InitiatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCreationRequests_Users_InitiatorId",
                table: "CourseCreationRequests",
                column: "InitiatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
