using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Create_Employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Restaurants",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressCode = table.Column<int>(type: "int", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAuths",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAuths", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeAuths_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_Email",
                table: "Restaurants",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerEmail",
                table: "Customers",
                column: "CustomerEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeEmail",
                table: "Employees",
                column: "EmployeeEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAuths");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_Email",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerEmail",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Restaurants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerEmail",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
