using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class AddInitiatorIdToCourseCreationDossier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("15dccf3e-bb40-4c83-9cc9-1a44c06442b8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("32834c9f-513c-472a-a58b-52973fe9ac0b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("351b6279-f0db-4ad7-8027-baffc048fbaf"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("420282c4-7604-4a62-baa7-3525b90ee356"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("59b2ac3a-b02c-47e7-9f67-087427e1a331"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("6a6dcdb5-d85e-4073-99ba-1dbb1004594b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("7867c383-c2c6-462d-8331-9c69506c31d3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8f9456d2-5b42-418d-8908-8a8bd8e99078"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("976ddabe-458a-4027-aab1-4a28b9323063"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9cead84f-9656-448e-9534-d007848ab8c8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("9f34a24c-586c-4102-8667-6a8efad8da4b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("afc3f669-562e-4359-8559-cc31ea6a83e5"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b6a02ff4-ab79-43a0-b8c8-de7d017d6e1b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ba2fe855-b30d-4df6-a6cc-c58820107d68"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ca63bbcb-0d6f-4731-a412-a60c96176d59"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cbb2b0d3-1706-479d-8d0e-8aaa728ddd2a"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("e672cc87-ee26-4b90-b34d-0103cdd3b739"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ecfb9ee8-35f9-4bed-9f96-769e0c5ab6c8"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f058d35d-a3e5-4cb7-89d7-857f76760bab"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "CourseComponents",
                columns: new[] { "Id", "ComponentCode", "ComponentName", "CreatedDate", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("15dccf3e-bb40-4c83-9cc9-1a44c06442b8"), 13, "Seminar", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1263), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1264) },
                    { new Guid("32834c9f-513c-472a-a58b-52973fe9ac0b"), 16, "Tutorial", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1280), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1281) },
                    { new Guid("351b6279-f0db-4ad7-8027-baffc048fbaf"), 7, "Online", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1215), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1216) },
                    { new Guid("420282c4-7604-4a62-baa7-3525b90ee356"), 12, "Research", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1257), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1258) },
                    { new Guid("59b2ac3a-b02c-47e7-9f67-087427e1a331"), 6, "Modular", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1209), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1209) },
                    { new Guid("6a6dcdb5-d85e-4073-99ba-1dbb1004594b"), 11, "Regular", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1251), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1252) },
                    { new Guid("7867c383-c2c6-462d-8331-9c69506c31d3"), 9, "Private Studies", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1230), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1231) },
                    { new Guid("8f9456d2-5b42-418d-8908-8a8bd8e99078"), 1, "Field Studies", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1147), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1148) },
                    { new Guid("976ddabe-458a-4027-aab1-4a28b9323063"), 15, "Thesis Research", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1275), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1275) },
                    { new Guid("9cead84f-9656-448e-9534-d007848ab8c8"), 0, "Conference", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1133), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1135) },
                    { new Guid("9f34a24c-586c-4102-8667-6a8efad8da4b"), 17, "Tutorial/Lab", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1288), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1289) },
                    { new Guid("afc3f669-562e-4359-8559-cc31ea6a83e5"), 3, "Independent Study", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1185), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1186) },
                    { new Guid("b6a02ff4-ab79-43a0-b8c8-de7d017d6e1b"), 2, "Fieldwork", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1179), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1179) },
                    { new Guid("ba2fe855-b30d-4df6-a6cc-c58820107d68"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1221), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1222) },
                    { new Guid("ca63bbcb-0d6f-4731-a412-a60c96176d59"), 18, "Workshop", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1301), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1302) },
                    { new Guid("cbb2b0d3-1706-479d-8d0e-8aaa728ddd2a"), 5, "Lecture", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1202), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1203) },
                    { new Guid("e672cc87-ee26-4b90-b34d-0103cdd3b739"), 10, "Reading", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1245), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1245) },
                    { new Guid("ecfb9ee8-35f9-4bed-9f96-769e0c5ab6c8"), 14, "Studio", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1269), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1270) },
                    { new Guid("f058d35d-a3e5-4cb7-89d7-857f76760bab"), 4, "Laboratory", new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1191), new DateTime(2023, 9, 19, 1, 43, 40, 211, DateTimeKind.Utc).AddTicks(1192) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8617), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8626), RoleEnum.Initiator },
                    { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8639), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8640), RoleEnum.Admin },
                    { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8646), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8647), RoleEnum.FacultyMember }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8828), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8829) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8872), new DateTime(2023, 9, 19, 1, 43, 40, 210, DateTimeKind.Utc).AddTicks(8872) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("3db4e489-e8a7-4e0e-a82b-e2221a8d802d"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("49bf28ac-97d0-4bbb-901e-a06206e4f626"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("b981ecac-ddf9-4a0a-9f55-7222fbecc65b"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });
        }
    }
}
