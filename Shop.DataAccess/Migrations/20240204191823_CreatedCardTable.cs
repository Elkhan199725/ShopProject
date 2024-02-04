using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class CreatedCardTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_CardNumber",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Wallets");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                table: "Wallets",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CardId",
                table: "Wallets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CvcCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CardId",
                table: "Wallets",
                column: "CardId",
                unique: true,
                filter: "[CardId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Card_CardNumber_CvcCode",
                table: "Card",
                columns: new[] { "CardNumber", "CvcCode" },
                unique: true,
                filter: "[CardNumber] IS NOT NULL AND [CvcCode] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Card_CardId",
                table: "Wallets",
                column: "CardId",
                principalTable: "Card",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Card_CardId",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_CardId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "CardId",
                table: "Wallets");

            migrationBuilder.AlterColumn<int>(
                name: "Balance",
                table: "Wallets",
                type: "int",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                table: "Wallets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Wallets",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_CardNumber",
                table: "Wallets",
                column: "CardNumber",
                unique: true,
                filter: "[CardNumber] IS NOT NULL");
        }
    }
}
