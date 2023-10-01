using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDossiersAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("05645422-fb0a-44f8-8ff4-9f61af1da4e3"), 18, "Workshop", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9930), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9930) },
                    { new Guid("0e1a407c-0c23-4c3d-a588-9dde4aac6520"), 9, "Private Studies", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9820), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9820) },
                    { new Guid("548132ac-b0d1-4e36-b012-d16b3991a1c1"), 6, "Modular", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9780), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9780) },
                    { new Guid("55d8715c-9369-4189-b4a9-22abb7aa5d47"), 2, "Fieldwork", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9730), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9730) },
                    { new Guid("58afcf7e-2f0f-4255-b45b-052c6832b495"), 12, "Research", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9850), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9850) },
                    { new Guid("5e5c944f-3cd8-4c4c-84d1-775b91000d8c"), 1, "Field Studies", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9720), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9720) },
                    { new Guid("69de14bc-161c-4053-b837-2b35ad50754f"), 5, "Lecture", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9770), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9770) },
                    { new Guid("7329d7f9-b6f6-4215-a1eb-fb63ad30b005"), 11, "Regular", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9840), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9840) },
                    { new Guid("7ce74307-0dd5-4c4b-b26d-73e0d0b2c657"), 7, "Online", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9790), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9790) },
                    { new Guid("87af9a12-32a6-41fb-ad33-5caab72418d1"), 4, "Laboratory", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9750), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9750) },
                    { new Guid("8c24287d-c2f5-4122-ae91-0a4a54252148"), 0, "Conference", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9700), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9700) },
                    { new Guid("90bc079a-377b-4b64-abf4-ecb246829162"), 14, "Studio", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9880), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9880) },
                    { new Guid("91eef5dd-bbe5-42a9-85aa-81331f75e600"), 17, "Tutorial/Lab", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9910), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9910) },
                    { new Guid("98e83551-c78f-4d37-a1e8-1e4b2c4a441b"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9800), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9800) },
                    { new Guid("a0c47a83-6d75-4606-a424-5d61f8a1c7c9"), 3, "Independent Study", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9740), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9740) },
                    { new Guid("b4358a82-c6fa-4bf3-97a3-ba5f2ca206eb"), 10, "Reading", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9830), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9830) },
                    { new Guid("c49f5303-a83b-45cc-8b7e-1e528a061991"), 16, "Tutorial", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9900), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9900) },
                    { new Guid("cc43b121-4574-45c1-9238-eba839b671da"), 15, "Thesis Research", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9890), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9890) },
                    { new Guid("dbec6d2c-d502-4de1-a972-4b5ac1eaca6c"), 13, "Seminar", new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9860), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9860) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3192d217-1b96-4da0-9a91-3baa339ff136"), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9160), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9160), RoleEnum.Initiator },
                    { new Guid("43c4b59e-e408-4766-8f9a-8d17ee8784e5"), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9190), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9190), RoleEnum.FacultyMember },
                    { new Guid("8a9d292e-94f7-4705-ab94-dd835b8a4c71"), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9180), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9180), RoleEnum.Admin }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9300), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9300) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9320), new DateTime(2023, 10, 1, 19, 27, 30, 589, DateTimeKind.Utc).AddTicks(9320) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3192d217-1b96-4da0-9a91-3baa339ff136"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("43c4b59e-e408-4766-8f9a-8d17ee8784e5"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("8a9d292e-94f7-4705-ab94-dd835b8a4c71"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("05645422-fb0a-44f8-8ff4-9f61af1da4e3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0e1a407c-0c23-4c3d-a588-9dde4aac6520"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("548132ac-b0d1-4e36-b012-d16b3991a1c1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("55d8715c-9369-4189-b4a9-22abb7aa5d47"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("58afcf7e-2f0f-4255-b45b-052c6832b495"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5e5c944f-3cd8-4c4c-84d1-775b91000d8c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("69de14bc-161c-4053-b837-2b35ad50754f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7329d7f9-b6f6-4215-a1eb-fb63ad30b005"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7ce74307-0dd5-4c4b-b26d-73e0d0b2c657"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("87af9a12-32a6-41fb-ad33-5caab72418d1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8c24287d-c2f5-4122-ae91-0a4a54252148"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("90bc079a-377b-4b64-abf4-ecb246829162"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("91eef5dd-bbe5-42a9-85aa-81331f75e600"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("98e83551-c78f-4d37-a1e8-1e4b2c4a441b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a0c47a83-6d75-4606-a424-5d61f8a1c7c9"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b4358a82-c6fa-4bf3-97a3-ba5f2ca206eb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("c49f5303-a83b-45cc-8b7e-1e528a061991"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cc43b121-4574-45c1-9238-eba839b671da"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("dbec6d2c-d502-4de1-a972-4b5ac1eaca6c"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3192d217-1b96-4da0-9a91-3baa339ff136"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("43c4b59e-e408-4766-8f9a-8d17ee8784e5"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("8a9d292e-94f7-4705-ab94-dd835b8a4c71"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3192d217-1b96-4da0-9a91-3baa339ff136"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("43c4b59e-e408-4766-8f9a-8d17ee8784e5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("8a9d292e-94f7-4705-ab94-dd835b8a4c71"));

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
    }
}
