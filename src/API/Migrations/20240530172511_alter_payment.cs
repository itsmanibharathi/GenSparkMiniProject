using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class alter_payment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CashPayments_CashPaymentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OnlinePaymentId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CashPaymentId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CashPayments_CashPaymentId",
                table: "Orders",
                column: "CashPaymentId",
                principalTable: "CashPayments",
                principalColumn: "CashPaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders",
                column: "OnlinePaymentId",
                principalTable: "Payments",
                principalColumn: "OnlinePaymentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_CashPayments_CashPaymentId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "OnlinePaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CashPaymentId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_CashPayments_CashPaymentId",
                table: "Orders",
                column: "CashPaymentId",
                principalTable: "CashPayments",
                principalColumn: "CashPaymentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_OnlinePaymentId",
                table: "Orders",
                column: "OnlinePaymentId",
                principalTable: "Payments",
                principalColumn: "OnlinePaymentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
