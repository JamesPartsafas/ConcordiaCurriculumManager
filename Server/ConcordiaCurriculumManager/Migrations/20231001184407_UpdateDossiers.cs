using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDossiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dossiers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("13e452ed-c215-4f58-828d-a8d78d492575"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6640), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6640) },
                    { new Guid("1cff55e5-23d5-4d78-9192-778b8b54f659"), 10, "Reading", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6670), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6670) },
                    { new Guid("1e3376f9-6749-4a84-88fa-83316fcf43a3"), 12, "Research", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6690), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6690) },
                    { new Guid("1ea9921e-6cd5-4aea-89a8-064f011db5b8"), 18, "Workshop", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6770), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6770) },
                    { new Guid("2505db77-c801-4590-b6a2-1180f1b434cb"), 16, "Tutorial", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6740), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6740) },
                    { new Guid("2e82a833-ff46-48d4-9d7c-def35c92ad65"), 15, "Thesis Research", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6730), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6730) },
                    { new Guid("5887e5a7-02cb-4158-bb7a-f96cebbdac7a"), 7, "Online", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6630), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6630) },
                    { new Guid("6b340274-6126-46b6-bdaa-2834ae655d06"), 17, "Tutorial/Lab", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6750), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6750) },
                    { new Guid("7043f6bc-d5d2-404e-ae3b-589d13671891"), 1, "Field Studies", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6560), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6560) },
                    { new Guid("720a4ccb-a0a4-4bb7-9002-0eda589f2b5d"), 6, "Modular", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6620), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6620) },
                    { new Guid("831b6bef-acfe-4bca-b57d-2ea052ee9dbd"), 14, "Studio", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6720), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6720) },
                    { new Guid("b837b339-3fce-49c7-9484-0df7fa66bf6b"), 2, "Fieldwork", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6570), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6570) },
                    { new Guid("baa7a2cb-a986-40b9-a134-529ea18723fc"), 11, "Regular", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6680), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6680) },
                    { new Guid("c3b748ac-6279-4c30-928e-f1a2cd387945"), 4, "Laboratory", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6590), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6590) },
                    { new Guid("e12709f5-e0a7-4797-a30c-ef3f307ffa5b"), 0, "Conference", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6540), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6540) },
                    { new Guid("e210eb56-d9ef-4f3a-9cf2-637a2236397d"), 9, "Private Studies", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6650), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6650) },
                    { new Guid("e4b93e49-1778-43b7-89de-74d52f5cea2b"), 3, "Independent Study", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6580), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6580) },
                    { new Guid("e9add686-c23e-4416-bf21-a9724a2a99d8"), 13, "Seminar", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6710), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6710) },
                    { new Guid("fc414fca-107a-44c9-a9d1-537b797e6c83"), 5, "Lecture", new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6610), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6610) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3d879b5a-f6a7-4913-9919-9b1e95526492"), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5950), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5950), RoleEnum.Admin },
                    { new Guid("e47345a4-ac5c-4d4b-b686-051868658899"), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5960), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5960), RoleEnum.FacultyMember },
                    { new Guid("eb8f66cc-a50a-44b2-aa28-e227eefa9383"), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5930), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(5930), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6120), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6140), new DateTime(2023, 10, 1, 18, 44, 6, 994, DateTimeKind.Utc).AddTicks(6140) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3d879b5a-f6a7-4913-9919-9b1e95526492"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("e47345a4-ac5c-4d4b-b686-051868658899"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("eb8f66cc-a50a-44b2-aa28-e227eefa9383"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("13e452ed-c215-4f58-828d-a8d78d492575"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1cff55e5-23d5-4d78-9192-778b8b54f659"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1e3376f9-6749-4a84-88fa-83316fcf43a3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1ea9921e-6cd5-4aea-89a8-064f011db5b8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("2505db77-c801-4590-b6a2-1180f1b434cb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("2e82a833-ff46-48d4-9d7c-def35c92ad65"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5887e5a7-02cb-4158-bb7a-f96cebbdac7a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6b340274-6126-46b6-bdaa-2834ae655d06"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7043f6bc-d5d2-404e-ae3b-589d13671891"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("720a4ccb-a0a4-4bb7-9002-0eda589f2b5d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("831b6bef-acfe-4bca-b57d-2ea052ee9dbd"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b837b339-3fce-49c7-9484-0df7fa66bf6b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("baa7a2cb-a986-40b9-a134-529ea18723fc"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c3b748ac-6279-4c30-928e-f1a2cd387945"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e12709f5-e0a7-4797-a30c-ef3f307ffa5b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e210eb56-d9ef-4f3a-9cf2-637a2236397d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e4b93e49-1778-43b7-89de-74d52f5cea2b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e9add686-c23e-4416-bf21-a9724a2a99d8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fc414fca-107a-44c9-a9d1-537b797e6c83"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3d879b5a-f6a7-4913-9919-9b1e95526492"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("e47345a4-ac5c-4d4b-b686-051868658899"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("eb8f66cc-a50a-44b2-aa28-e227eefa9383"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3d879b5a-f6a7-4913-9919-9b1e95526492"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("e47345a4-ac5c-4d4b-b686-051868658899"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("eb8f66cc-a50a-44b2-aa28-e227eefa9383"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Dossiers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

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
    }
}
