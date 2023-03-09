using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastucture.Migrations
{
    public partial class addingdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "registeredUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailConfirm = table.Column<bool>(type: "bit", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    confirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_registeredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "assignTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    registeredUsersId = table.Column<int>(type: "int", nullable: false),
                    tasksId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    isChecked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignTasks_registeredUsers_registeredUsersId",
                        column: x => x.registeredUsersId,
                        principalTable: "registeredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_assignTasks_tasks_tasksId",
                        column: x => x.tasksId,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userSuccesses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    assignTasksId = table.Column<int>(type: "int", nullable: false),
                    success = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userSuccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_userSuccesses_assignTasks_assignTasksId",
                        column: x => x.assignTasksId,
                        principalTable: "assignTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignTasks_registeredUsersId",
                table: "assignTasks",
                column: "registeredUsersId");

            migrationBuilder.CreateIndex(
                name: "IX_assignTasks_tasksId",
                table: "assignTasks",
                column: "tasksId");

            migrationBuilder.CreateIndex(
                name: "IX_userSuccesses_assignTasksId",
                table: "userSuccesses",
                column: "assignTasksId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userSuccesses");

            migrationBuilder.DropTable(
                name: "assignTasks");

            migrationBuilder.DropTable(
                name: "registeredUsers");

            migrationBuilder.DropTable(
                name: "tasks");
        }
    }
}
