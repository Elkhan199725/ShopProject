using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class ModifedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddresses_Users_UserId",
                table: "DeliveryAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DeliveryAddresses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddresses_Users_UserId",
                table: "DeliveryAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAddresses_Users_UserId",
                table: "DeliveryAddresses");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "DeliveryAddresses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAddresses_Users_UserId",
                table: "DeliveryAddresses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
