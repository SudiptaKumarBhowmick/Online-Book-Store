using Microsoft.EntityFrameworkCore.Migrations;

namespace DLL.Migrations
{
    public partial class ShipmentTableUpdateMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceNumber",
                table: "Shipments");

            migrationBuilder.AddColumn<string>(
                name: "AddressLineOne",
                table: "Shipments",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressLineTwo",
                table: "Shipments",
                type: "varchar(1000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Shipments",
                type: "varchar(20)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Shipments",
                type: "varchar(50)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Shipments",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Shipments",
                type: "varchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Shipments",
                type: "varchar(20)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressLineOne",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "AddressLineTwo",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Shipments");

            migrationBuilder.AddColumn<string>(
                name: "InvoiceNumber",
                table: "Shipments",
                type: "varchar(10)",
                nullable: true);
        }
    }
}
