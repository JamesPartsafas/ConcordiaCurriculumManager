using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class MakeCourseReferenceOptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseReferences_CourseID",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseID",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReferences",
                table: "CourseReferences");

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

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "CourseReferences");

            migrationBuilder.AlterColumn<string>(
                name: "EquivalentCourses",
                table: "Courses",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseReferences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CourseReferencedId",
                table: "CourseReferences",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "CourseReferences",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "CourseReferences",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReferences",
                table: "CourseReferences",
                column: "Id");

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

            migrationBuilder.CreateIndex(
                name: "IX_CourseReferences_CourseReferencedId",
                table: "CourseReferences",
                column: "CourseReferencedId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseReferences_Courses_CourseReferencedId",
                table: "CourseReferences",
                column: "CourseReferencedId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseReferences_Courses_CourseReferencedId",
                table: "CourseReferences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseReferences",
                table: "CourseReferences");

            migrationBuilder.DropIndex(
                name: "IX_CourseReferences_CourseReferencedId",
                table: "CourseReferences");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseReferences");

            migrationBuilder.DropColumn(
                name: "CourseReferencedId",
                table: "CourseReferences");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "CourseReferences");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "CourseReferences");

            migrationBuilder.AlterColumn<string>(
                name: "EquivalentCourses",
                table: "Courses",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "CourseReferences",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseReferences",
                table: "CourseReferences",
                column: "CourseID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseID",
                table: "Courses",
                column: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseReferences_CourseID",
                table: "Courses",
                column: "CourseID",
                principalTable: "CourseReferences",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
