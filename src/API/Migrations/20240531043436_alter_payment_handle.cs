using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class alter_payment_handle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashPayments_Employees_EmployeeId",
                table: "CashPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_EmployeeId",
                table: "CashPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CashPayments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "OnlinePayments");

            migrationBuilder.RenameColumn(
                name: "PayAmount",
                table: "CashPayments",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "PayAmount",
                table: "OnlinePayments",
                newName: "PaymentAmount");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OnlinePayments",
                newName: "CustomerId");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveBy",
                table: "CashPayments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "CashPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "OnlinePayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OnlinePayments",
                table: "OnlinePayments",
                column: "OnlinePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_ReceiveBy",
                table: "CashPayments",
                column: "ReceiveBy");

            migrationBuilder.CreateIndex(
                name: "IX_OnlinePayments_CustomerId",
                table: "OnlinePayments",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashPayments_Employees_ReceiveBy",
                table: "CashPayments",
                column: "ReceiveBy",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OnlinePayments_Customers_CustomerId",
                table: "OnlinePayments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OnlinePayments_OnlinePaymentId",
                table: "Orders",
                column: "OnlinePaymentId",
                principalTable: "OnlinePayments",
                principalColumn: "OnlinePaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashPayments_Employees_ReceiveBy",
                table: "CashPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_OnlinePayments_Customers_CustomerId",
                table: "OnlinePayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OnlinePayments_OnlinePaymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CashPayments_ReceiveBy",
                table: "CashPayments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OnlinePayments",
                table: "OnlinePayments");

            migrationBuilder.DropIndex(
                name: "IX_OnlinePayments_CustomerId",
                table: "OnlinePayments");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "CashPayments");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "OnlinePayments");

            migrationBuilder.RenameTable(
                name: "OnlinePayments",
                newName: "Payments");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "CashPayments",
                newName: "PayAmount");

            migrationBuilder.RenameColumn(
                name: "PaymentAmount",
                table: "Payments",
                newName: "PayAmount");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Payments",
                newName: "OrderId");

            migrationBuilder.AlterColumn<int>(
                name: "ReceiveBy",
                table: "CashPayments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CashPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CashPayments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "OnlinePaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_CashPayments_EmployeeId",
                table: "CashPayments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CashPayments_Employees_EmployeeId",
                table: "CashPayments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders",
                column: "OnlinePaymentId",
                principalTable: "Payments",
                principalColumn: "OnlinePaymentId");
        }
    }
}
