using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCoolCarSystem.Data.Migrations
{
    public partial class Purchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchase_Cars_CarId",
                table: "CarPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_CarPurchase_Customers_CustomerId",
                table: "CarPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarPurchase",
                table: "CarPurchase");

            migrationBuilder.RenameTable(
                name: "CarPurchase",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_CarPurchase_CarId",
                table: "Purchases",
                newName: "IX_Purchases_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                columns: new[] { "CustomerId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Cars_CarId",
                table: "Purchases",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Customers_CustomerId",
                table: "Purchases",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Cars_CarId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Customers_CustomerId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "CarPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_CarId",
                table: "CarPurchase",
                newName: "IX_CarPurchase_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarPurchase",
                table: "CarPurchase",
                columns: new[] { "CustomerId", "CarId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchase_Cars_CarId",
                table: "CarPurchase",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarPurchase_Customers_CustomerId",
                table: "CarPurchase",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
