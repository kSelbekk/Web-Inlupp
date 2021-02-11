using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_Inlupp.Data.Migrations
{
    public partial class quant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Quantity",
                table: "ShoppingCart",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingCart");
        }
    }
}
