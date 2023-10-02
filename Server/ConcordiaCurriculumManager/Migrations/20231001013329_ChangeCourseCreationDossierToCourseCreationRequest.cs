using System;
using ConcordiaCurriculumManager.Models.Users;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConcordiaCurriculumManager.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCourseCreationDossierToCourseCreationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCreationDossiers");

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

            migrationBuilder.CreateTable(
                name: "CourseCreationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewCourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    DossierId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCreationRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCreationRequests_Courses_NewCourseId",
                        column: x => x.NewCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCreationRequests_Dossiers_DossierId",
                        column: x => x.DossierId,
                        principalTable: "Dossiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCreationRequests_Users_InitiatorId",
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
                    { new Guid("33253aa4-68db-4542-86e7-483fe1fbffc0"), 14, "Studio", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3667), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3668) },
                    { new Guid("356c8039-c7c2-41cb-88fe-3d599d61c4d3"), 2, "Fieldwork", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3622), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3622) },
                    { new Guid("51af3bee-a03b-4275-8fa0-0b05cbdbe583"), 4, "Laboratory", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3639), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3640) },
                    { new Guid("5ae01d9c-c388-448c-b8a0-8e6c7920bcc4"), 9, "Private Studies", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3654), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3654) },
                    { new Guid("5ca40d42-780c-4258-bc2d-c7e7b2daa65b"), 7, "Online", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3648), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3649) },
                    { new Guid("674708bd-54ff-4ef3-bbc9-ba4e8925ca13"), 1, "Field Studies", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3619), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3619) },
                    { new Guid("811316e2-b37c-41e2-acf9-ac0265c5832d"), 3, "Independent Study", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3624), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3625) },
                    { new Guid("8f6def6a-192a-4411-9aa6-88eaea8e664d"), 12, "Research", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3663), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3663) },
                    { new Guid("940d9e6f-8db1-43fb-9a42-7f862ac124bb"), 6, "Modular", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3646), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3646) },
                    { new Guid("a2577e32-f488-4111-af58-58751ae0baca"), 11, "Regular", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3659), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3659) },
                    { new Guid("b0476aff-0008-4077-a7b6-bf9a95acb0fa"), 17, "Tutorial/Lab", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3675), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3675) },
                    { new Guid("bdb5c77c-4abb-49d7-a4c4-4f0dfc887e71"), 13, "Seminar", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3665), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3665) },
                    { new Guid("cf3964d0-a3d1-4df0-9527-7046bda7e901"), 18, "Workshop", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3677), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3677) },
                    { new Guid("ebc8bd19-adab-4aa1-ab62-9b9bc32f9359"), 5, "Lecture", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3643), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3644) },
                    { new Guid("ef00b09c-36b3-46c3-b1bf-348c7f6e76d0"), 15, "Thesis Research", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3670), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3670) },
                    { new Guid("f08ac39f-b0e6-487a-896c-6c7fbc671fb2"), 8, "Practicum/Internship/Work-Term", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3651), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3651) },
                    { new Guid("f1e721ad-94d7-4230-90c5-38a423ac5bfb"), 16, "Tutorial", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3672), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3672) },
                    { new Guid("fb51146f-0f5f-4874-be77-4d6dbdc17700"), 0, "Conference", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3611), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3612) },
                    { new Guid("fe68260e-19eb-438d-95fa-ce8b2e7d2046"), 10, "Reading", new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3656), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3656) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedDate", "ModifiedDate", "UserRole" },
                values: new object[,]
                {
                    { new Guid("0fcc7db8-6901-4f3a-a96c-0e0813f49169"), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2894), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2894), RoleEnum.Admin },
                    { new Guid("57362097-1c44-4b27-9044-123274e957a4"), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2897), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2897), RoleEnum.FacultyMember },
                    { new Guid("a2f15b84-c948-445d-905e-5e31edede8e9"), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2881), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(2884), RoleEnum.Initiator }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("37581d9d-713f-475c-9668-23971b0e64d0"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3001), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3002) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a"),
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3023), new DateTime(2023, 10, 1, 1, 33, 29, 249, DateTimeKind.Utc).AddTicks(3024) });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[,]
                {
                    { new Guid("0fcc7db8-6901-4f3a-a96c-0e0813f49169"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") },
                    { new Guid("57362097-1c44-4b27-9044-123274e957a4"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") },
                    { new Guid("a2f15b84-c948-445d-905e-5e31edede8e9"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationRequests_DossierId",
                table: "CourseCreationRequests",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationRequests_InitiatorId",
                table: "CourseCreationRequests",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationRequests_NewCourseId",
                table: "CourseCreationRequests",
                column: "NewCourseId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCreationRequests");

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("33253aa4-68db-4542-86e7-483fe1fbffc0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("356c8039-c7c2-41cb-88fe-3d599d61c4d3"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("51af3bee-a03b-4275-8fa0-0b05cbdbe583"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5ae01d9c-c388-448c-b8a0-8e6c7920bcc4"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("5ca40d42-780c-4258-bc2d-c7e7b2daa65b"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("674708bd-54ff-4ef3-bbc9-ba4e8925ca13"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("811316e2-b37c-41e2-acf9-ac0265c5832d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("8f6def6a-192a-4411-9aa6-88eaea8e664d"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("940d9e6f-8db1-43fb-9a42-7f862ac124bb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("a2577e32-f488-4111-af58-58751ae0baca"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("b0476aff-0008-4077-a7b6-bf9a95acb0fa"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("bdb5c77c-4abb-49d7-a4c4-4f0dfc887e71"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("cf3964d0-a3d1-4df0-9527-7046bda7e901"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ebc8bd19-adab-4aa1-ab62-9b9bc32f9359"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("ef00b09c-36b3-46c3-b1bf-348c7f6e76d0"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f08ac39f-b0e6-487a-896c-6c7fbc671fb2"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("f1e721ad-94d7-4230-90c5-38a423ac5bfb"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fb51146f-0f5f-4874-be77-4d6dbdc17700"));

            migrationBuilder.DeleteData(
                table: "CourseComponents",
                keyColumn: "Id",
                keyValue: new Guid("fe68260e-19eb-438d-95fa-ce8b2e7d2046"));

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("0fcc7db8-6901-4f3a-a96c-0e0813f49169"), new Guid("37581d9d-713f-475c-9668-23971b0e64d0") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("57362097-1c44-4b27-9044-123274e957a4"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { new Guid("a2f15b84-c948-445d-905e-5e31edede8e9"), new Guid("8c55b0c3-b4cf-4948-a730-dad3fa37c69a") });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0fcc7db8-6901-4f3a-a96c-0e0813f49169"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("57362097-1c44-4b27-9044-123274e957a4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a2f15b84-c948-445d-905e-5e31edede8e9"));

            migrationBuilder.CreateTable(
                name: "CourseCreationDossiers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    InitiatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewCourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCreationDossiers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCreationDossiers_Courses_NewCourseId",
                        column: x => x.NewCourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCreationDossiers_Users_InitiatorId",
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
                name: "IX_CourseCreationDossiers_InitiatorId",
                table: "CourseCreationDossiers",
                column: "InitiatorId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCreationDossiers_NewCourseId",
                table: "CourseCreationDossiers",
                column: "NewCourseId",
                unique: true);
        }
    }
}
