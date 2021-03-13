using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class WishlistColumnUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_User_UserId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Wishlists",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_User_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_User_UserId",
                table: "Wishlists");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Wishlists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_User_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
