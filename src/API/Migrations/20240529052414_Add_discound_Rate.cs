using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    public partial class Add_discound_Rate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalTax",
                table: "Orders",
                newName: "TaxRat");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Orders",
                newName: "DiscountRat");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxRat",
                table: "Orders",
                newName: "TotalTax");

            migrationBuilder.RenameColumn(
                name: "DiscountRat",
                table: "Orders",
                newName: "DiscountPrice");
        }
    }
}
