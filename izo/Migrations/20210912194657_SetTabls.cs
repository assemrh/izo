using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace izo.Migrations
{
    public partial class SetTabls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GsmNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExamResults",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamResults", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ExamResults_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExamResults_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2d32ef16-a1f5-484f-ae08-880ce97e9cd6"), "Example Course 1" },
                    { new Guid("7a0315ef-c614-40e0-b66c-1a629e5b1025"), "Example Course 2" },
                    { new Guid("b8b83056-625e-480b-8264-8348a5d31d04"), "Example Course 3" },
                    { new Guid("6985ca2a-5394-4b07-9408-e2a7e50ce75e"), "Example Course 4" },
                    { new Guid("3e1b71ed-34b7-48fb-ae65-f8a0722b3abf"), "Example Course 5" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FullName", "GsmNumber", "Number" },
                values: new object[,]
                {
                    { new Guid("121aab8d-56da-42be-b699-34cce87368d7"), null, "David C. Hunter", null, 0 },
                    { new Guid("e9079439-4306-4f8e-ae4e-6a1363025dc4"), null, "Carlos Waterslide", null, 0 },
                    { new Guid("46793e18-d77d-46d3-8ef1-687526405d7c"), null, "Luke Sexgator", null, 0 },
                    { new Guid("55369196-c3fe-4313-98ca-6a7262c04775"), null, "Hector McHector", null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role", "Username" },
                values: new object[] { new Guid("ee55933d-bd9b-4cbe-a6be-16f0ec75422c"), "admin@domain.tld", "Admin", "Usr@12345", "Admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_CourseID",
                table: "ExamResults",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_ExamResults_StudentId",
                table: "ExamResults",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExamResults");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
