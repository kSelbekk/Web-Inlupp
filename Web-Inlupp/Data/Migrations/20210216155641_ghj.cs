using Microsoft.EntityFrameworkCore.Migrations;

namespace Web_Inlupp.Data.Migrations
{
    public partial class ghj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Products_ProductNameId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductNameId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ProductNameId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShoppingCart",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_Products_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCart_ProductId",
                table: "ShoppingCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShoppingCart");

            migrationBuilder.AddColumn<int>(
                name: "ProductNameId",
                table: "ShoppingCart",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCart_ProductNameId",
                table: "ShoppingCart",
                column: "ProductNameId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_Products_ProductNameId",
                table: "ShoppingCart",
                column: "ProductNameId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
