using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class ModifiedWholeSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Card_Wallets_WalletId",
                table: "Card");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Card",
                table: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Card_CardNumber_CvcCode",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "CvcCode",
                table: "Card");

            migrationBuilder.RenameTable(
                name: "Card",
                newName: "Cards");

            migrationBuilder.RenameIndex(
                name: "IX_Card_WalletId",
                table: "Cards",
                newName: "IX_Cards_WalletId");

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Wallets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId1",
                table: "Wallets",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Cards",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CardHolderName",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CardBalance",
                table: "Cards",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Cvc",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cards",
                table: "Cards",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CardId1",
                table: "Wallets",
                column: "CardId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards",
                columns: new[] { "CardNumber", "Cvc" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_UserId",
                table: "Cards",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Wallets_WalletId",
                table: "Cards",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Cards_CardId1",
                table: "Wallets",
                column: "CardId1",
                principalTable: "Cards",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_UserId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Wallets_WalletId",
                table: "Cards");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Cards_CardId1",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CardId1",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cards",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_UserId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardId1",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardBalance",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "Cvc",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cards");

            migrationBuilder.RenameTable(
                name: "Cards",
                newName: "Card");

            migrationBuilder.RenameIndex(
                name: "IX_Cards_WalletId",
                table: "Card",
                newName: "IX_Card_WalletId");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Card",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CardHolderName",
                table: "Card",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CvcCode",
                table: "Card",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Card",
                table: "Card",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber_CvcCode",
                table: "Card",
                columns: new[] { "CardNumber", "CvcCode" },
                unique: true,
                filter: "[CardNumber] IS NOT NULL AND [CvcCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Wallets_WalletId",
                table: "Card",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }
    }
}
