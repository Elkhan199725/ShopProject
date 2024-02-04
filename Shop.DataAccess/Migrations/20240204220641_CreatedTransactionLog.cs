using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class CreatedTransactionLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "CardBalance",
                table: "Cards",
                newName: "Balance");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Cards",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CardHolderName",
                table: "Cards",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "TransactionLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CardId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionLogs_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TransactionLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards",
                columns: new[] { "CardNumber", "Cvc" },
                unique: true,
                filter: "[CardNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_CardId",
                table: "TransactionLogs",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLogs_UserId",
                table: "TransactionLogs",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionLogs");

            migrationBuilder.DropIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards");

            migrationBuilder.RenameColumn(
                name: "Balance",
                table: "Cards",
                newName: "CardBalance");

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

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardNumber_Cvc",
                table: "Cards",
                columns: new[] { "CardNumber", "Cvc" },
                unique: true);
        }
    }
}
