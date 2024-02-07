using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class CreatedRelationInvoiceandCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "Cvc",
                table: "Cards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "InvoiceId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards",
                columns: new[] { "CardNumber", "Cvc" },
                unique: true,
                filter: "[CardNumber] IS NOT NULL AND [Cvc] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_InvoiceId",
                table: "Cards",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Invoices_InvoiceId",
                table: "Cards",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Invoices_InvoiceId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_InvoiceId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Cards");

            migrationBuilder.AlterColumn<int>(
                name: "Cvc",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards",
                columns: new[] { "CardNumber", "Cvc" },
                unique: true,
                filter: "[CardNumber] IS NOT NULL");
        }
    }
}
