using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class ProductInStockUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInStocks_Products_ProductId",
                table: "ProductInStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductInStocks_ProductId",
                table: "ProductInStocks");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInStocks",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStocks_ProductId",
                table: "ProductInStocks",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInStocks_Products_ProductId",
                table: "ProductInStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInStocks_Products_ProductId",
                table: "ProductInStocks");

            migrationBuilder.DropIndex(
                name: "IX_ProductInStocks_ProductId",
                table: "ProductInStocks");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductInStocks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ProductInStocks_ProductId",
                table: "ProductInStocks",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInStocks_Products_ProductId",
                table: "ProductInStocks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
