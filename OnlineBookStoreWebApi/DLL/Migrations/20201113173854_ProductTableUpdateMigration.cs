using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class ProductTableUpdateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategory_ProductCategroyId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategroyId",
                table: "Products",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategory_ProductCategroyId",
                table: "Products",
                column: "ProductCategroyId",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategroyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductCategory_ProductCategroyId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ProductCategroyId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductCategory_ProductCategroyId",
                table: "Products",
                column: "ProductCategroyId",
                principalTable: "ProductCategory",
                principalColumn: "ProductCategroyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
