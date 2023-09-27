using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDossierToDossiers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dossier_Users_InitiatorId",
                table: "Dossier");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dossier",
                table: "Dossier");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("1b272084-8e74-4ce7-ba3b-a33bade66481"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("27fadeb9-fcc9-4cbc-9371-8fcc1e24fa9f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5067fbc1-b08c-44b2-b0b8-8e3348364466"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("57ed0216-ebb5-4951-8387-1ad4f1df1887"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("631932be-93cb-4434-af6d-646258632671"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6922e123-59ab-4e2c-aa2c-eb31bf98896c"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6dc73ec7-4149-44c6-8633-5b01fa08b713"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("739e01dd-7c25-487a-bf15-3d12a9190d38"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("79058e71-abb4-4f8a-8ae1-eadf0b286e5b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("86c3bd81-31e4-4796-83b0-baa5c8445bcd"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("896aeb25-d471-41bc-9c2a-2b8939b74321"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9124cc18-db0c-4a87-b17a-f1055c40b9ec"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9e0d1ea6-7e23-40c4-92a2-607aef3f3d5b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a2b9f1b4-de6a-4771-aa52-f4c16b2416f1"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ac63cb18-bdb6-47c3-928a-9f517b32042f"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d103f7a7-f2c0-4168-980b-8cdb8e8ca4ec"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("d2c853b0-e8ad-4544-b9ee-0e7fd2b26e51"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("edcb4f9f-2e6e-4430-87fd-539c33779da9"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fb8f36a6-f8b1-4aea-aedb-10757b9f6cf4"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3150867c-4666-4347-be48-515d6e3bbc59"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("394dc872-fa07-4e2f-a8cb-65e12d552f5a"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("9639f0f2-bc1d-4f84-a6c8-d2868ed3ae38"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3150867c-4666-4347-be48-515d6e3bbc59"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("394dc872-fa07-4e2f-a8cb-65e12d552f5a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9639f0f2-bc1d-4f84-a6c8-d2868ed3ae38"));

            migrationBuilder.RenameTable(
                name: "Dossier",
                newName: "Dossiers");

            migrationBuilder.RenameIndex(
                name: "IX_Dossier_InitiatorId",
                table: "Dossiers",
                newName: "IX_Dossiers_InitiatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dossiers",
                table: "Dossiers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("0281d9a7-856a-4cb5-95a9-fd57a8945cb2"), 7, "Online", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7680), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7680) },
                    { new Guid("0ac8015e-54ff-4325-bfa8-730ad5b9b9ed"), 5, "Lecture", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660) },
                    { new Guid("1b8ca8ec-7c28-4141-9cfb-976add3d213b"), 3, "Independent Study", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7650), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7650) },
                    { new Guid("24105b87-b50d-44a7-a9b4-b649564b8524"), 6, "Modular", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7670), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7670) },
                    { new Guid("2d6f6f05-584a-480a-b3ab-03b8c25e57aa"), 11, "Regular", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750) },
                    { new Guid("377c7121-9e41-4811-b89e-e3c06a1adfbd"), 18, "Workshop", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7800), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7810) },
                    { new Guid("4e5fbc1b-281d-4b6e-90b6-ad9355ee2cdb"), 4, "Laboratory", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7660) },
                    { new Guid("606e42c9-7683-4d0d-993b-e99692f7e318"), 10, "Reading", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7750) },
                    { new Guid("9232e88e-6b4d-47e9-8a2d-02778090d974"), 15, "Thesis Research", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780) },
                    { new Guid("9cdf4c72-d146-4c5f-9393-e8dbe45faf13"), 9, "Private Studies", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7740), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7740) },
                    { new Guid("ac7a7a14-a0f9-44bb-8196-9e1c3cb74f90"), 13, "Seminar", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7770), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7770) },
                    { new Guid("afbe189c-1066-4d2c-b832-5c48023fcbc5"), 14, "Studio", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7780) },
                    { new Guid("b6380285-b4aa-4f72-8da2-4851380f515a"), 1, "Field Studies", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630) },
                    { new Guid("b7d29dae-b7ae-42f6-a24d-63dc133ce692"), 16, "Tutorial", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7790), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7790) },
                    { new Guid("b8d6ec66-07ec-46d3-93bf-7954048bd43c"), 12, "Research", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7760), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7760) },
                    { new Guid("c9094880-fdfc-4c5d-84a1-4703968eac25"), 2, "Fieldwork", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7640), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7640) },
                    { new Guid("df1606a6-3b8a-44b9-aa9d-78e637647c9d"), 0, "Conference", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7630) },
                    { new Guid("faaa880f-6bfe-4f9d-9128-78799ffad237"), 17, "Tutorial/Lab", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7800), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7800) },
                    { new Guid("fdcde47e-b63f-489e-b2da-99154398b069"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7690), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7690) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("66c43e77-69be-43ef-af72-7d6475621604"), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7290), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7290), RoleEnum.Initiator },
                    { new Guid("bcd0948b-6c9f-40b9-994e-a69f0e5bddac"), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7310), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7310), RoleEnum.FacultyMember },
                    { new Guid("fa9b2620-6366-4f47-b7a4-1d502f9cf9df"), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7300), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7300), RoleEnum.Admin }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7380), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7380) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7390), new DateTime(2023, 9, 24, 20, 50, 20, 934, DateTimeKind.Utc).AddTicks(7390) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("66c43e77-69be-43ef-af72-7d6475621604"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("bcd0948b-6c9f-40b9-994e-a69f0e5bddac"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("fa9b2620-6366-4f47-b7a4-1d502f9cf9df"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dossiers_Users_InitiatorId",
                table: "Dossiers",
                column: "InitiatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dossiers_Users_InitiatorId",
                table: "Dossiers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dossiers",
                table: "Dossiers");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0281d9a7-856a-4cb5-95a9-fd57a8945cb2"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("0ac8015e-54ff-4325-bfa8-730ad5b9b9ed"));

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
                keyValue: new Guid("377c7121-9e41-4811-b89e-e3c06a1adfbd"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("4e5fbc1b-281d-4b6e-90b6-ad9355ee2cdb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("606e42c9-7683-4d0d-993b-e99692f7e318"));

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
                keyValue: new Guid("ac7a7a14-a0f9-44bb-8196-9e1c3cb74f90"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("afbe189c-1066-4d2c-b832-5c48023fcbc5"));

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
                keyValue: new Guid("b8d6ec66-07ec-46d3-93bf-7954048bd43c"));

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
                keyValue: new Guid("faaa880f-6bfe-4f9d-9128-78799ffad237"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fdcde47e-b63f-489e-b2da-99154398b069"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("66c43e77-69be-43ef-af72-7d6475621604"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("bcd0948b-6c9f-40b9-994e-a69f0e5bddac"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("fa9b2620-6366-4f47-b7a4-1d502f9cf9df"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("66c43e77-69be-43ef-af72-7d6475621604"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bcd0948b-6c9f-40b9-994e-a69f0e5bddac"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fa9b2620-6366-4f47-b7a4-1d502f9cf9df"));

            migrationBuilder.RenameTable(
                name: "Dossiers",
                newName: "Dossier");

            migrationBuilder.RenameIndex(
                name: "IX_Dossiers_InitiatorId",
                table: "Dossier",
                newName: "IX_Dossier_InitiatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dossier",
                table: "Dossier",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("1b272084-8e74-4ce7-ba3b-a33bade66481"), 14, "Studio", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1720), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1720) },
                    { new Guid("27fadeb9-fcc9-4cbc-9371-8fcc1e24fa9f"), 17, "Tutorial/Lab", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1740), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1740) },
                    { new Guid("5067fbc1-b08c-44b2-b0b8-8e3348364466"), 5, "Lecture", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1650), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1650) },
                    { new Guid("57ed0216-ebb5-4951-8387-1ad4f1df1887"), 7, "Online", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1670), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1670) },
                    { new Guid("631932be-93cb-4434-af6d-646258632671"), 11, "Regular", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1700), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1700) },
                    { new Guid("6922e123-59ab-4e2c-aa2c-eb31bf98896c"), 1, "Field Studies", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1620), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1620) },
                    { new Guid("6dc73ec7-4149-44c6-8633-5b01fa08b713"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1680), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1680) },
                    { new Guid("739e01dd-7c25-487a-bf15-3d12a9190d38"), 4, "Laboratory", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1640), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1640) },
                    { new Guid("79058e71-abb4-4f8a-8ae1-eadf0b286e5b"), 13, "Seminar", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1710), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1710) },
                    { new Guid("86c3bd81-31e4-4796-83b0-baa5c8445bcd"), 3, "Independent Study", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1640), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1640) },
                    { new Guid("896aeb25-d471-41bc-9c2a-2b8939b74321"), 18, "Workshop", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1750), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1750) },
                    { new Guid("9124cc18-db0c-4a87-b17a-f1055c40b9ec"), 0, "Conference", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1610), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1610) },
                    { new Guid("9e0d1ea6-7e23-40c4-92a2-607aef3f3d5b"), 12, "Research", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1710), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1710) },
                    { new Guid("a2b9f1b4-de6a-4771-aa52-f4c16b2416f1"), 10, "Reading", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1690), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1690) },
                    { new Guid("ac63cb18-bdb6-47c3-928a-9f517b32042f"), 2, "Fieldwork", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1630), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1630) },
                    { new Guid("d103f7a7-f2c0-4168-980b-8cdb8e8ca4ec"), 9, "Private Studies", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1680), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1680) },
                    { new Guid("d2c853b0-e8ad-4544-b9ee-0e7fd2b26e51"), 6, "Modular", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1660), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1660) },
                    { new Guid("edcb4f9f-2e6e-4430-87fd-539c33779da9"), 16, "Tutorial", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1740), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1740) },
                    { new Guid("fb8f36a6-f8b1-4aea-aedb-10757b9f6cf4"), 15, "Thesis Research", new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1730), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1730) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3150867c-4666-4347-be48-515d6e3bbc59"), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1270), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1270), RoleEnum.FacultyMember },
                    { new Guid("394dc872-fa07-4e2f-a8cb-65e12d552f5a"), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1270), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1270), RoleEnum.Admin },
                    { new Guid("9639f0f2-bc1d-4f84-a6c8-d2868ed3ae38"), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1250), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1260), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1360), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1360) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1370), new DateTime(2023, 9, 24, 20, 46, 56, 383, DateTimeKind.Utc).AddTicks(1370) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3150867c-4666-4347-be48-515d6e3bbc59"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("394dc872-fa07-4e2f-a8cb-65e12d552f5a"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("9639f0f2-bc1d-4f84-a6c8-d2868ed3ae38"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dossier_Users_InitiatorId",
                table: "Dossier",
                column: "InitiatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
