using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Alter_Cash_Add_OrderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_CashPaymentId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CashPayments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashPaymentId",
                table: "Orders",
                column: "CashPaymentId",
                unique: true,
                filter: "[CashPaymentId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_CashPaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CashPayments");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CashPaymentId",
                table: "Orders",
                column: "CashPaymentId");
        }
    }
}
