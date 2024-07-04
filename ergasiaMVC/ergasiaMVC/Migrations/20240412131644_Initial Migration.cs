using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ergasiaMVC.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    AFM = table.Column<int>(type: "int", nullable: false),
                    USERSusername = table.Column<string>(name: "USERS_username", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.AFM);
                    table.ForeignKey(
                        name: "FK_Professors_Users_USERS_username",
                        column: x => x.USERSusername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Secretaries",
                columns: table => new
                {
                    Phonenumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    USERSusername = table.Column<string>(name: "USERS_username", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Secretaries", x => x.Phonenumber);
                    table.ForeignKey(
                        name: "FK_Secretaries_Users_USERS_username",
                        column: x => x.USERSusername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    USERSusername = table.Column<string>(name: "USERS_username", type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.RegistrationNumber);
                    table.ForeignKey(
                        name: "FK_Students_Users_USERS_username",
                        column: x => x.USERSusername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    idCOURSE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseTitle = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    CourseSemester = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    PROFESSORSAFM = table.Column<int>(name: "PROFESSORS_AFM", type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.idCOURSE);
                    table.ForeignKey(
                        name: "FK_Courses_Professors_PROFESSORS_AFM",
                        column: x => x.PROFESSORSAFM,
                        principalTable: "Professors",
                        principalColumn: "AFM");
                });

            migrationBuilder.CreateTable(
                name: "courses_have_students",
                columns: table => new
                {
                    COURSEidCOURSE = table.Column<int>(name: "COURSE_idCOURSE", type: "int", nullable: false),
                    STUDENTSRegistrationNumber = table.Column<int>(name: "STUDENTS_RegistrationNumber", type: "int", nullable: false),
                    GradeCourseStudent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses_have_students", x => new { x.COURSEidCOURSE, x.STUDENTSRegistrationNumber });
                    table.ForeignKey(
                        name: "FK_courses_have_students_Courses_COURSE_idCOURSE",
                        column: x => x.COURSEidCOURSE,
                        principalTable: "Courses",
                        principalColumn: "idCOURSE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_courses_have_students_Students_STUDENTS_RegistrationNumber",
                        column: x => x.STUDENTSRegistrationNumber,
                        principalTable: "Students",
                        principalColumn: "RegistrationNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PROFESSORS_AFM",
                table: "Courses",
                column: "PROFESSORS_AFM");

            migrationBuilder.CreateIndex(
                name: "IX_courses_have_students_STUDENTS_RegistrationNumber",
                table: "courses_have_students",
                column: "STUDENTS_RegistrationNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_USERS_username",
                table: "Professors",
                column: "USERS_username");

            migrationBuilder.CreateIndex(
                name: "IX_Secretaries_USERS_username",
                table: "Secretaries",
                column: "USERS_username");

            migrationBuilder.CreateIndex(
                name: "IX_Students_USERS_username",
                table: "Students",
                column: "USERS_username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses_have_students");

            migrationBuilder.DropTable(
                name: "Secretaries");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
