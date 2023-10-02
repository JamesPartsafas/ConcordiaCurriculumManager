using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToDossiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("05f44b42-60cc-4b7b-9c9a-7155f2e02b77"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("11ed757c-e78e-4976-a0c2-df020589feb4"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("120613a0-b0ca-420f-98b9-9595786253e5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1c314d45-66ad-4940-ab4a-4db888d4bd03"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("202bf127-aa1c-4d73-b299-fe0e0ee6e870"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("45c332ea-194a-4188-818c-e83ad5bf27fd"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5673b664-19ef-4310-9974-591ce1b28fae"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7900cb88-e1fa-4d19-8a6e-ac209143ef7c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8b0ca142-0557-4e9d-9790-5a3351621ae5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9a476a2d-a1b9-4ebc-8137-1616563ca3ad"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c58dea6f-674f-41e9-9f4c-9773fa3ecb0b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cc11932a-18b6-4b60-a1d2-e8aee6ed82d0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ce741335-dee2-4016-bc55-079412462524"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d165dc5d-38fd-4d4f-b8b9-eee89c63e120"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d566a8f5-5f2f-4474-a211-4bd3659fa723"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d9cd8bd5-01a3-433a-b07e-5c91919e33df"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("da3cce51-355c-4155-81ee-94b6723533b0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e4578eb5-cb0e-4559-8977-4634f51bf685"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f897d433-3492-478e-9b78-9a2290ae2b2d"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9e2718e5-56d7-498d-ac6b-82a440a000b4"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("e8083566-ff8c-4b6b-946a-77c97d0964b6"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("f0ad73eb-fdbc-4169-b431-c249d37b3ec0"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9e2718e5-56d7-498d-ac6b-82a440a000b4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e8083566-ff8c-4b6b-946a-77c97d0964b6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f0ad73eb-fdbc-4169-b431-c249d37b3ec0"));

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    { new Guid("05f44b42-60cc-4b7b-9c9a-7155f2e02b77"), 17, "Tutorial/Lab", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7397), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7398) },
                    { new Guid("11ed757c-e78e-4976-a0c2-df020589feb4"), 1, "Field Studies", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7257), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7258) },
                    { new Guid("120613a0-b0ca-420f-98b9-9595786253e5"), 15, "Thesis Research", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7383), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7384) },
                    { new Guid("1c314d45-66ad-4940-ab4a-4db888d4bd03"), 4, "Laboratory", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7301), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7302) },
                    { new Guid("202bf127-aa1c-4d73-b299-fe0e0ee6e870"), 0, "Conference", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7240), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7244) },
                    { new Guid("45c332ea-194a-4188-818c-e83ad5bf27fd"), 3, "Independent Study", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7294), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7295) },
                    { new Guid("5673b664-19ef-4310-9974-591ce1b28fae"), 6, "Modular", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7317), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7318) },
                    { new Guid("7900cb88-e1fa-4d19-8a6e-ac209143ef7c"), 2, "Fieldwork", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7263), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7264) },
                    { new Guid("8b0ca142-0557-4e9d-9790-5a3351621ae5"), 7, "Online", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7324), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7325) },
                    { new Guid("9a476a2d-a1b9-4ebc-8137-1616563ca3ad"), 9, "Private Studies", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7340), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7341) },
                    { new Guid("c58dea6f-674f-41e9-9f4c-9773fa3ecb0b"), 10, "Reading", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7346), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7347) },
                    { new Guid("cc11932a-18b6-4b60-a1d2-e8aee6ed82d0"), 5, "Lecture", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7311), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7311) },
                    { new Guid("ce741335-dee2-4016-bc55-079412462524"), 12, "Research", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7366), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7366) },
                    { new Guid("d165dc5d-38fd-4d4f-b8b9-eee89c63e120"), 14, "Studio", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7377), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7378) },
                    { new Guid("d566a8f5-5f2f-4474-a211-4bd3659fa723"), 16, "Tutorial", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7389), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7390) },
                    { new Guid("d9cd8bd5-01a3-433a-b07e-5c91919e33df"), 18, "Workshop", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7403), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7404) },
                    { new Guid("da3cce51-355c-4155-81ee-94b6723533b0"), 11, "Regular", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7360), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7361) },
                    { new Guid("e4578eb5-cb0e-4559-8977-4634f51bf685"), 13, "Seminar", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7372), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7372) },
                    { new Guid("f897d433-3492-478e-9b78-9a2290ae2b2d"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7331), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(7332) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("9e2718e5-56d7-498d-ac6b-82a440a000b4"), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5141), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5142), RoleEnum.FacultyMember },
                    { new Guid("e8083566-ff8c-4b6b-946a-77c97d0964b6"), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5133), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5135), RoleEnum.Admin },
                    { new Guid("f0ad73eb-fdbc-4169-b431-c249d37b3ec0"), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5115), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5121), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5321), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5322) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5454), new DateTime(2023, 10, 2, 0, 43, 50, 236, DateTimeKind.Utc).AddTicks(5455) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("9e2718e5-56d7-498d-ac6b-82a440a000b4"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("e8083566-ff8c-4b6b-946a-77c97d0964b6"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("f0ad73eb-fdbc-4169-b431-c249d37b3ec0"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
