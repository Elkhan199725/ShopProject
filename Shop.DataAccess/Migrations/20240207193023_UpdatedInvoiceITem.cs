using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class UpdatedInvoiceITem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "InvoiceItems",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_InvoiceId",
                table: "Users",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Invoices_InvoiceId",
                table: "Users",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Invoices_InvoiceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_InvoiceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "InvoiceItems",
                newName: "Price");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Invoices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Invoices",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
