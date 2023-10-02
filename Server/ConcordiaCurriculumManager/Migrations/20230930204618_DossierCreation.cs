using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class DossierCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0281d9a7-856a-4cb5-95a9-fd57a8945cb2"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("04528cec-fb9d-4a41-bf7c-179578d8dba7"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0ac8015e-54ff-4325-bfa8-730ad5b9b9ed"));

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
                keyValue: new Guid("1b8ca8ec-7c28-4141-9cfb-976add3d213b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("24105b87-b50d-44a7-a9b4-b649564b8524"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("2d6f6f05-584a-480a-b3ab-03b8c25e57aa"));

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
                keyValue: new Guid("4e5fbc1b-281d-4b6e-90b6-ad9355ee2cdb"));

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
                keyValue: new Guid("606e42c9-7683-4d0d-993b-e99692f7e318"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6b3c8951-44cc-47b5-83fe-9db88bbbc8af"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9232e88e-6b4d-47e9-8a2d-02778090d974"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9cdf4c72-d146-4c5f-9393-e8dbe45faf13"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9ea71f9b-6cdb-4a1c-8731-29e9c944b314"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ac7a7a14-a0f9-44bb-8196-9e1c3cb74f90"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("afbe189c-1066-4d2c-b832-5c48023fcbc5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b2a436eb-a49e-49ee-9161-a85170c7982c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b6380285-b4aa-4f72-8da2-4851380f515a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b7d29dae-b7ae-42f6-a24d-63dc133ce692"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b817e1e2-f52e-493d-a258-fa1f0a712b3f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b8d6ec66-07ec-46d3-93bf-7954048bd43c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("bf1214e1-9333-4d85-bd32-d48cae4e5000"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c9094880-fdfc-4c5d-84a1-4703968eac25"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("df1606a6-3b8a-44b9-aa9d-78e637647c9d"));

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
                keyValue: new Guid("faaa880f-6bfe-4f9d-9128-78799ffad237"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fb6ef9a9-4cca-4409-857d-44735684d3db"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fbdc7787-bf80-4139-8501-6b9309059f21"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fdcde47e-b63f-489e-b2da-99154398b069"));

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

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Dossiers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Published",
                table: "Dossiers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("15a22992-5346-4afb-a4d9-9dbc381169d5"), 13, "Seminar", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7280), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7280) },
                    { new Guid("18cb9287-708f-4e97-bbf7-e575e9bc1c69"), 7, "Online", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7230), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7230) },
                    { new Guid("2feb923c-cf64-4065-b6bd-95ec55754042"), 4, "Laboratory", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7210), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7210) },
                    { new Guid("5515576d-562a-401e-b163-7df95bb261c3"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7240), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7240) },
                    { new Guid("6cb4f25f-22ac-491a-9d60-491711043097"), 1, "Field Studies", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7190), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7190) },
                    { new Guid("7303d94e-9100-4f23-a9ef-0c4d7bc8edde"), 15, "Thesis Research", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7290), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7290) },
                    { new Guid("8bba87b6-2e85-4023-a4f4-7dd4aae77c1c"), 14, "Studio", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7280), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7280) },
                    { new Guid("938ce338-31c1-4a29-ad71-46a9aed3fc34"), 17, "Tutorial/Lab", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7310), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7310) },
                    { new Guid("93bc8f88-cd46-4902-a6fc-b03eff9588dc"), 5, "Lecture", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7220), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7220) },
                    { new Guid("9c714af3-b538-4ab5-a33f-a25f81e5023a"), 2, "Fieldwork", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7190), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7190) },
                    { new Guid("a0573ccc-3e62-4ef4-9c68-5fa05da58d95"), 9, "Private Studies", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7250), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7250) },
                    { new Guid("a2518737-c96e-437f-936a-4ebccfecd5a7"), 10, "Reading", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7250), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7250) },
                    { new Guid("a5ff0e55-3d0a-4485-8ad6-ef69fbbd9bd6"), 11, "Regular", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7260), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7260) },
                    { new Guid("a6c0aa59-2898-40e7-af5e-a5d3495e3958"), 6, "Modular", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7220), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7220) },
                    { new Guid("ab732879-0326-457a-a9f3-36b129243c0f"), 3, "Independent Study", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7200), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7200) },
                    { new Guid("c3481245-3c4a-4007-9f0a-f32623113c7c"), 12, "Research", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7270), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7270) },
                    { new Guid("e4aa5e30-c717-4ffa-98a7-2967cd089aa1"), 16, "Tutorial", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7300), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7300) },
                    { new Guid("e8711e58-600d-43eb-824c-66b846c4aaff"), 0, "Conference", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7180), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7180) },
                    { new Guid("ed384c50-8591-40e3-a3b2-ac9d283d0c44"), 18, "Workshop", new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7310), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(7310) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3dd23161-b555-4e7e-b3fe-940df30a6540"), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6840), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6840), RoleEnum.Admin },
                    { new Guid("3dec37e6-b32e-433f-b3db-d1ba12f1bad3"), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6850), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6850), RoleEnum.FacultyMember },
                    { new Guid("9c7f7238-82fe-47cf-9a79-a8de164ae2d1"), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6830), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6830), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6920), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6920) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6940), new DateTime(2023, 9, 30, 20, 46, 18, 65, DateTimeKind.Utc).AddTicks(6940) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3dd23161-b555-4e7e-b3fe-940df30a6540"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("3dec37e6-b32e-433f-b3db-d1ba12f1bad3"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("9c7f7238-82fe-47cf-9a79-a8de164ae2d1"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("15a22992-5346-4afb-a4d9-9dbc381169d5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("18cb9287-708f-4e97-bbf7-e575e9bc1c69"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("2feb923c-cf64-4065-b6bd-95ec55754042"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5515576d-562a-401e-b163-7df95bb261c3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6cb4f25f-22ac-491a-9d60-491711043097"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7303d94e-9100-4f23-a9ef-0c4d7bc8edde"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8bba87b6-2e85-4023-a4f4-7dd4aae77c1c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("938ce338-31c1-4a29-ad71-46a9aed3fc34"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("93bc8f88-cd46-4902-a6fc-b03eff9588dc"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9c714af3-b538-4ab5-a33f-a25f81e5023a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a0573ccc-3e62-4ef4-9c68-5fa05da58d95"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a2518737-c96e-437f-936a-4ebccfecd5a7"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a5ff0e55-3d0a-4485-8ad6-ef69fbbd9bd6"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a6c0aa59-2898-40e7-af5e-a5d3495e3958"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ab732879-0326-457a-a9f3-36b129243c0f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c3481245-3c4a-4007-9f0a-f32623113c7c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e4aa5e30-c717-4ffa-98a7-2967cd089aa1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e8711e58-600d-43eb-824c-66b846c4aaff"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ed384c50-8591-40e3-a3b2-ac9d283d0c44"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3dd23161-b555-4e7e-b3fe-940df30a6540"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3dec37e6-b32e-433f-b3db-d1ba12f1bad3"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9c7f7238-82fe-47cf-9a79-a8de164ae2d1"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dd23161-b555-4e7e-b3fe-940df30a6540"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3dec37e6-b32e-433f-b3db-d1ba12f1bad3"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9c7f7238-82fe-47cf-9a79-a8de164ae2d1"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Dossiers");

            migrationBuilder.DropColumn(
                name: "Published",
                table: "Dossiers");

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("0281d9a7-856a-4cb5-95a9-fd57a8945cb2"), 7, "Online", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7680), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7680) },
                    { new Guid("04528cec-fb9d-4a41-bf7c-179578d8dba7"), 1, "Field Studies", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8849), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8850) },
                    { new Guid("0ac8015e-54ff-4325-bfa8-730ad5b9b9ed"), 5, "Lecture", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660) },
                    { new Guid("0ebce308-810c-48e6-baaa-7144195a4429"), 2, "Fieldwork", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8852), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8852) },
                    { new Guid("13321ab8-9a57-4ad6-a236-7f567274991b"), 17, "Tutorial/Lab", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8900), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8901) },
                    { new Guid("1b8ca8ec-7c28-4141-9cfb-976add3d213b"), 3, "Independent Study", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7650), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7650) },
                    { new Guid("24105b87-b50d-44a7-a9b4-b649564b8524"), 6, "Modular", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7670), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7670) },
                    { new Guid("2d6f6f05-584a-480a-b3ab-03b8c25e57aa"), 11, "Regular", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750) },
                    { new Guid("42399ad1-d4e1-4dc5-b53e-a3bfd3dc21bf"), 18, "Workshop", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8903), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8903) },
                    { new Guid("440a9fd4-50ce-40fa-a130-29062059ba10"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8875), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8876) },
                    { new Guid("47b79ccc-8a33-439e-a0d9-ca4022c56b05"), 0, "Conference", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8843), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8844) },
                    { new Guid("4d092dd4-a32c-485b-8ef9-b785bd09db10"), 11, "Regular", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8885), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8886) },
                    { new Guid("4e5fbc1b-281d-4b6e-90b6-ad9355ee2cdb"), 4, "Laboratory", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660) },
                    { new Guid("503ab4ba-1ee0-46e6-940c-6a5259d7fb5a"), 14, "Studio", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8893), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8893) },
                    { new Guid("5857b83a-126d-4dbf-aa5f-8d8d117e6473"), 10, "Reading", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8880), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8881) },
                    { new Guid("606e42c9-7683-4d0d-993b-e99692f7e318"), 10, "Reading", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750) },
                    { new Guid("6b3c8951-44cc-47b5-83fe-9db88bbbc8af"), 13, "Seminar", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8890), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8891) },
                    { new Guid("9232e88e-6b4d-47e9-8a2d-02778090d974"), 15, "Thesis Research", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780) },
                    { new Guid("9cdf4c72-d146-4c5f-9393-e8dbe45faf13"), 9, "Private Studies", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7740), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7740) },
                    { new Guid("9ea71f9b-6cdb-4a1c-8731-29e9c944b314"), 7, "Online", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8873), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8874) },
                    { new Guid("ac7a7a14-a0f9-44bb-8196-9e1c3cb74f90"), 13, "Seminar", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7770), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7770) },
                    { new Guid("afbe189c-1066-4d2c-b832-5c48023fcbc5"), 14, "Studio", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780) },
                    { new Guid("b2a436eb-a49e-49ee-9161-a85170c7982c"), 4, "Laboratory", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8866), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8866) },
                    { new Guid("b6380285-b4aa-4f72-8da2-4851380f515a"), 1, "Field Studies", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630) },
                    { new Guid("b7d29dae-b7ae-42f6-a24d-63dc133ce692"), 16, "Tutorial", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7790), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7790) },
                    { new Guid("b817e1e2-f52e-493d-a258-fa1f0a712b3f"), 12, "Research", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8888), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8888) },
                    { new Guid("b8d6ec66-07ec-46d3-93bf-7954048bd43c"), 12, "Research", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7760), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7760) },
                    { new Guid("bf1214e1-9333-4d85-bd32-d48cae4e5000"), 6, "Modular", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8871), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8872) },
                    { new Guid("c9094880-fdfc-4c5d-84a1-4703968eac25"), 2, "Fieldwork", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7640), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7640) },
                    { new Guid("df1606a6-3b8a-44b9-aa9d-78e637647c9d"), 0, "Conference", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630) },
                    { new Guid("e033e74c-8a24-4914-9d32-82649906ebd9"), 16, "Tutorial", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8898), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8898) },
                    { new Guid("f22ef1bb-4670-44de-a547-0652f9065392"), 3, "Independent Study", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8864), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8864) },
                    { new Guid("f7246d4c-c0de-4b0b-9f98-753d8781d5ee"), 15, "Thesis Research", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8895), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8895) },
                    { new Guid("faaa880f-6bfe-4f9d-9128-78799ffad237"), 17, "Tutorial/Lab", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7800), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7800) },
                    { new Guid("fb6ef9a9-4cca-4409-857d-44735684d3db"), 5, "Lecture", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8869), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8870) },
                    { new Guid("fbdc7787-bf80-4139-8501-6b9309059f21"), 9, "Private Studies", new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8878), new DateTime(2023, 9, 23, 21, 18, 39, 0, DateTimeKind.Utc).AddTicks(8878) },
                    { new Guid("fdcde47e-b63f-489e-b2da-99154398b069"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7690), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7690) }
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
        }
    }
}
