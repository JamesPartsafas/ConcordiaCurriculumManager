using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class CreateDossiers : Migration
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
                name: "Dossier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dossier_Users_InitiatorId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Dossier_InitiatorId",
                table: "Dossier",
                column: "InitiatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dossier");

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
