using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class ModifiedWallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Card_CardId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CardId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Wallets");

            migrationBuilder.CreateIndex(
                name: "IX_Card_WalletId",
                table: "Card",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Wallets_WalletId",
                table: "Card",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Wallets_WalletId",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_WalletId",
                table: "Card");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Wallets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CardId",
                table: "Wallets",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Card_CardId",
                table: "Wallets",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");
        }
    }
}
