using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_PropductId",
                table: "Baskets");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.RenameColumn(
                name: "PaymantMethod",
                table: "Invoices",
                newName: "PaymentMethod");

            migrationBuilder.RenameColumn(
                name: "DiscountPercenatage",
                table: "Discounts",
                newName: "DiscountPercentage");

            migrationBuilder.RenameColumn(
                name: "PropductId",
                table: "Baskets",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_PropductId",
                table: "Baskets",
                newName: "IX_Baskets_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Products_ProductId",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Invoices",
                newName: "PaymantMethod");

            migrationBuilder.RenameColumn(
                name: "DiscountPercentage",
                table: "Discounts",
                newName: "DiscountPercenatage");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Baskets",
                newName: "PropductId");

            migrationBuilder.RenameIndex(
                name: "IX_Baskets_ProductId",
                table: "Baskets",
                newName: "IX_Baskets_PropductId");

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AdminName_AdminEmail",
                table: "Admins",
                columns: new[] { "AdminName", "AdminEmail" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Products_PropductId",
                table: "Baskets",
                column: "PropductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
