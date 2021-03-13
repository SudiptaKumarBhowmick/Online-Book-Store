using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class ProductDescriptionColumnUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDescriptions_Products_ProductId",
                table: "ProductDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductDescriptions_ProductId",
                table: "ProductDescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductDescriptions",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescriptions_ProductId",
                table: "ProductDescriptions",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDescriptions_Products_ProductId",
                table: "ProductDescriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductDescriptions_Products_ProductId",
                table: "ProductDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_ProductDescriptions_ProductId",
                table: "ProductDescriptions");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductDescriptions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_ProductDescriptions_ProductId",
                table: "ProductDescriptions",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDescriptions_Products_ProductId",
                table: "ProductDescriptions",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
